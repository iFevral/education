using Dapper;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories.DapperRepositories
{
    public class OrderRepository : DapperBaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(IConfiguration configuration) : base(configuration)
        { }

        public async Task<DataModel<Order>> GetAllOrders(OrderFilterModel filterModel)
        {
            var sql = new StringBuilder($@"SELECT o.*, u.FirstName, u.LastName, u.Email, oi.Amount, pe.Id, pe.Title, pe.Type
                                            FROM (SELECT *
	                                              FROM Orders
	                                              LEFT JOIN (Select OrderId, SUM(OrderItems.Amount * PrintingEditions.Price) as OrderPrice
				                                             FROM OrderItems
				                                             LEFT JOIN PrintingEditions ON OrderItems.PrintingEditionId = PrintingEditions.Id
				                                             GROUP BY OrderId) AS op ON Orders.Id = op.OrderId
	                                              WHERE (Status IN @Statuses) AND
                                                        (isRemoved = @IsRemoved) ");
            if (filterModel.UserId != null)
            {
                sql.Append(new StringBuilder($@"        AND (UserId = @UserId)"));
            }
            sql.Append(new StringBuilder($@"      ORDER BY {filterModel.SortProperty.ToString()} { (filterModel.IsAscending ? "ASC" : "DESC") }
		                                                OFFSET @Offset ROWS"));
            if (filterModel.Quantity > 0)
            {
                sql.Append(new StringBuilder($@"	    FETCH NEXT @Quantity ROWS ONLY"));
            }

            sql.Append(new StringBuilder($@") AS o
                                            LEFT JOIN Users AS u ON o.UserId = u.Id
                                            LEFT JOIN OrderItems AS oi ON oi.OrderId = o.Id
                                            LEFT JOIN PrintingEditions AS pe ON oi.PrintingEditionId = pe.Id;"));

            var sqlCounter = new StringBuilder($@"SELECT COUNT(*)
                                                  FROM Orders
                                                  WHERE (Status IN @Statuses) AND
                                                       (IsRemoved = @IsRemoved) ");
            if (filterModel.UserId != null)
            {
                sqlCounter.Append(new StringBuilder($@"AND (UserId = { filterModel.UserId })"));
            }

            sql.Append(sqlCounter);

            using (var databaseConnection = new SqlConnection(_connectionString))
            {
                var queryResult = await databaseConnection.QueryMultipleAsync(sql.ToString(),
                    new
                    {
                        Statuses = filterModel.Statuses,
                        IsRemoved = 0,
                        UserId = filterModel.UserId,
                        Offset = filterModel.StartIndex,
                        Quantity = filterModel.Quantity
                    });

                var dataModel = new DataModel<Order>();

                var orders = queryResult.Read<Order, decimal?, User, OrderItem, PrintingEdition, Order>(               
                    (order, orderPrice, user, orderItem, printingEdition) =>
                    {
                        order.OrderPrice = orderPrice == null ? 0 : orderPrice;
                        user.ConcurrencyStamp = null;
                        order.User = user;
                        
                        if(orderItem == null)
                        {
                            return order;
                        }

                        orderItem.PrintingEdition = printingEdition;

                        order.OrderItems = new List<OrderItem>();
                        order.OrderItems.Add(orderItem);

                        return order;
                    },
                    splitOn: "OrderPrice, FirstName, Amount, Id"
                );

                dataModel.Items = orders.GroupBy(order =>
                    new
                    {
                        order.Id,
                        order.Description,
                        order.OrderPrice,
                        order.PaymentId,
                        order.Status,
                        order.User,
                        order.UserId
                    }
                )
                    .Select(group => new Order
                    {
                        Id = group.Key.Id,
                        Description = group.Key.Description,
                        OrderPrice = group.Key.OrderPrice,
                        Status = group.Key.Status,
                        UserId = group.Key.UserId,
                        User = group.Key.User,
                        OrderItems = group.Select(order => order.OrderItems.FirstOrDefault()).ToList()
                    });

                dataModel.Counter = await queryResult.ReadFirstAsync<int>();

                return dataModel;
            }
        }
    }
}