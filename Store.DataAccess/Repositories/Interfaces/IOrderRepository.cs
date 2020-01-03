using System.Threading.Tasks;
using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Base;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public Task<DataModel<Order>> GetAllOrders(OrderFilterModel filterModel);
    }
}