using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IOrderItemRepository : IGenericRepository<OrderItems>
    {
        public Task<bool> RemoveByOrderIdAsync(int id);
    }
}
