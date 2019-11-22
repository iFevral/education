using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class OrderItemRepository : EFBaseRepository<OrderItems>, IOrderItemRepository
    {
        private readonly ApplicationContext _db;
        public OrderItemRepository(ApplicationContext db) : base(db)
        {
            _db = db;
        }
    }
}
