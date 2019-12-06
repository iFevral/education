using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.AppContext;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class OrderRepository : EFBaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
