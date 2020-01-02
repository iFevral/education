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

        public async Task<ListModel<OrderModel>> GetAllOrders(OrderFilterModel filterModel)
        {
            var sql = new StringBuilder($@"SELECT o.*, u.FirstName, u.LastName, u.Email, oi.Amount, pe.Id, pe.Title, pe.Type
                                            FROM (SELECT *
	                                              FROM Orders
	                                              LEFT JOIN (Select OrderId, SUM(OrderItems.Amount * PrintingEditions.Price) as OrderPrice
				                                             FROM OrderItems
				                                             LEFT JOIN PrintingEditions ON OrderItems.PrintingEditionId = PrintingEditions.Id
				                                             GROUP BY OrderId) AS op ON Orders.Id = op.OrderId
	                                              WHERE (Status IN ({ string.Join(",", filterModel.Statuses) })) AND
                                                        (isRemoved = 0) ");
            if (filterModel.UserId != null)
            {
                sql.Append(new StringBuilder($@"        AND (UserId = { filterModel.UserId })"));
            }
            sql.Append(new StringBuilder($@"      ORDER BY { filterModel.SortProperty } { (filterModel.IsAscending ? "ASC" : "DESC") }
		                                                OFFSET { filterModel.StartIndex } ROWS"));
            if (filterModel.Quantity > 0)
            {
                sql.Append(new StringBuilder($@"	    FETCH NEXT { filterModel.Quantity } ROWS ONLY"));
            } 

            sql.Append(new StringBuilder($@") AS o
                                            LEFT JOIN Users AS u ON o.UserId = u.Id
                                            LEFT JOIN OrderItems AS oi ON oi.OrderId = o.Id
                                            LEFT JOIN PrintingEditions AS pe ON oi.PrintingEditionId = pe.Id;"));

            var sqlCounter = new StringBuilder($@"SELECT COUNT(*)
                                                  FROM Orders
                                                 WHERE (Status IN ({ string.Join(",", filterModel.Statuses) })) AND
                                                       (IsRemoved = 0) ");
            if (filterModel.UserId != null)
            {
                sqlCounter.Append(new StringBuilder($@"AND (UserId = { filterModel.UserId })"));
            }

            sql.Append(sqlCounter);

            using (var databaseConnection = new SqlConnection(_connectionString))
            {
                var queryResult = await databaseConnection.QueryMultipleAsync(sql.ToString());

                var list = new ListModel<OrderModel>();
                list.Items = new List<OrderModel>();

                queryResult.Read<OrderModel, decimal, User, OrderItem, PrintingEdition, OrderModel>(
                    (order, orderPrice, user, orderItem, printingEdition) =>
                    {
                        order.OrderPrice = orderPrice;
                        order.User = user;

                        if (list.Items.Count() == 0 || list.Items.Last().Id != order.Id)
                        {
                            list.Items.AsList().Add(order);
                        }


                        if (list.Items.Last().OrderItems == null)
                        {
                            list.Items.Last().OrderItems = new List<OrderItem>();
                        }

                        if (orderItem == null)
                        {
                            return order;
                        }

                        orderItem.PrintingEdition = printingEdition;

                        list.Items.Last().OrderItems.Add(orderItem);

                        return order;
                    },
                    splitOn: "OrderPrice, FirstName, Amount, Id"
                );

                list.Counter = await queryResult.ReadFirstAsync<int>();

                return list;
            }
        }
    }
}