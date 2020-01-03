using System.Linq;
using Store.DataAccess.Entities;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Extensions.Sorting;
using Store.DataAccess.Repositories.Interfaces;
using System.Threading.Tasks;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class OrderRepository : EFBaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<DataModel<Order>> GetAllOrders(OrderFilterModel filterModel)
        {
            var list = new DataModel<Order>();
            list.Items = _dbSet.Where(filterModel.Predicate)
                               .AsEnumerable()
                               .GroupBy(order => order)
                               .Select(group => new Order
                               {
                                   Id = group.Key.Id,
                                   CreationDate = group.Key.CreationDate,
                                   Description = group.Key.Description,
                                   isRemoved = group.Key.isRemoved,
                                   Status = group.Key.Status,
                                   User = group.Key.User,
                                   UserId = group.Key.UserId,
                                   Payment = group.Key.Payment,
                                   PaymentId = group.Key.PaymentId,
                                   OrderItems = group.Key.OrderItems,
                                   OrderPrice = group.Key.OrderItems.Sum(item => item.Amount * item.PrintingEdition.Price)
                               })
                               .SortBy(filterModel.SortProperty.ToString(), filterModel.IsAscending);

            list.Counter = list.Items.Count();

            if (filterModel.Quantity > 0)
            {
                list.Items = list.Items.Skip(filterModel.StartIndex).Take(filterModel.Quantity);
            }

            return list;
        }
    }
}
