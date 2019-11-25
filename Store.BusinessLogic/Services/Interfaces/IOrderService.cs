using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Payments;
using Store.DataAccess.Entities;
using System;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Get part orders or all orders with filtering
        /// </summary>
        /// <returns>All orders</returns>
        public Task<OrderModel> GetAll(OrderFilter orderFilter, int startIndex, int quantity);

        /// <summary>
        /// Get order by order id
        /// </summary>
        /// <returns>Order with id</returns>
        public Task<OrderModel> FindById(int id);

        /// <summary>
        /// Get order by order id and username
        /// </summary>
        /// <returns>Order with id</returns>
        public OrderModel FindById(int id, string username);

        /// <summary>
        /// Create order
        /// </summary>
        public Task<OrderModel> Create(OrderInputData modelItem);

        /// <summary>
        /// Edit order
        /// </summary>
        public Task<OrderModel> Update(int id, OrderInputData modelItem);

        /// <summary>
        /// Delete order
        /// </summary>
        public Task<OrderModel> Delete(int id);

        /// <summary>
        /// Create payment add add to order
        /// </summary>
        public Task<OrderModel> AddPaymentTransaction(int OrderId, PaymentModelItem modelItem);

        /// <summary>
        /// Remove payment from order and delete
        /// </summary>
        public Task<OrderModel> RemovePaymentTransaction(int OrderId);
    }
}
