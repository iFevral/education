using Store.DataAccess.Entities;
using Store.DataAccess.Models.EFFilters;
using Store.DataAccess.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public Task<IEnumerable<Order>> GetAllSortedByAmount(FilterModel<Order> filterModel);
    }
}