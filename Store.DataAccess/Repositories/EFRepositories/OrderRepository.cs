using System.Linq;
using System.Collections.Generic;
using Store.DataAccess.Entities;
using Store.DataAccess.AppContext;
using Store.DataAccess.Models.EFFilters;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Extensions.Sorting;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class OrderRepository : EFBaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Order> GetAllSortedByAmount(FilterModel<Order> filterModel, out int counter)
        {
            var items = _dbSet.Where(filterModel.Predicate)
                               .AsEnumerable()
                               .SortByOrderAmount(filterModel.IsAscending);

            counter = items.Count();

            if (filterModel.Quantity > 0)
            {
                items = items.Skip(filterModel.StartIndex).Take(filterModel.Quantity);
            }

            return items.ToList();
        }
    }
}
