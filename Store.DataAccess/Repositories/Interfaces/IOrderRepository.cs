using Store.DataAccess.Entities;
using Store.DataAccess.Models.EFFilters;
using Store.DataAccess.Repositories.Base;
using System.Collections.Generic;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public IEnumerable<Order> GetAllSortedByAmount(FilterModel<Order> filterModel, out int counter);
    }
}