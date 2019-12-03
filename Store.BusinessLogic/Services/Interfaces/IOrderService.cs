using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Payments;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Get part orders or all orders with filtering
        /// </summary>
        /// <returns>All orders</returns>
        public Task<OrderModel> GetAllAsync(OrderFilter orderFilter);

        /// <summary>
        /// Get order by order id
        /// </summary>
        /// <returns>Order with id</returns>
        public Task<OrderModelItem> FindByIdAsync(int id);

        /// <summary>
        /// Create order
        /// </summary>
        public Task<OrderModelItem> CreateAsync(OrderModelItem modelItem);

        /// <summary>
        /// Edit order
        /// </summary>
        public Task<OrderModelItem> UpdateAsync(int id, OrderModelItem modelItem);

        /// <summary>
        /// Delete order
        /// </summary>
        public Task<OrderModelItem> DeleteAsync(int id);

        /// <summary>
        /// Create payment add add to order
        /// </summary>
        public Task<OrderModelItem> AddPaymentTransactionAsync(int OrderId, PaymentModelItem modelItem);

        /// <summary>
        /// Remove payment from order and delete
        /// </summary>
        public Task<OrderModelItem> RemovePaymentTransactionAsync(int OrderId);

    }
}
