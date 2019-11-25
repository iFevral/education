using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class OrderItemRepository : EFBaseRepository<OrderItems>, IOrderItemRepository
    {
        private readonly ApplicationContext _db;
        public OrderItemRepository(ApplicationContext db) : base(db)
        {
            _db = db;
        }

        public async Task RemoveByOrderId(int id)
        {
            _db.OrderItems.RemoveRange(_db.OrderItems.Where(oi => oi.OrderId == id));
            await _db.SaveChangesAsync();
        }
    }
}
