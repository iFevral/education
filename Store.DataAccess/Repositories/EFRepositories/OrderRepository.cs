using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.AppContext;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class OrderRepository : EFBaseRepository<Order>, IOrderRepository
    {
        private readonly ApplicationContext _db;

        public OrderRepository(ApplicationContext db) : base(db)
        {
            _db = db;
        }
    }
}
