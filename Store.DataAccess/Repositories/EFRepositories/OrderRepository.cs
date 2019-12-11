using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.AppContext;
using System.Threading.Tasks;
using System.Collections.Generic;
using Store.DataAccess.Models.EFFilters;
using System.Linq;
using Store.DataAccess.Extensions.Sorting;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class OrderRepository : EFBaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Order>> GetAllSortedByAmount(FilterModel<Order> filterModel)
        {
            var items = _dbSet.Where(filterModel.Predicate)
                               .AsEnumerable()
                               .SortByOrderAmount(filterModel.IsAscending);

            if (filterModel.Quantity > 0)
            {
                items = items.Skip(filterModel.StartIndex).Take(filterModel.Quantity);
            }

            return items.ToList();
        }
    }
}
