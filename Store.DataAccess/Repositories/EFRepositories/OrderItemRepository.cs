using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class OrderItemRepository : EFBaseRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
