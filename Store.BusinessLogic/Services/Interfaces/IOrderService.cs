using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.Filters;
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
        public Task<OrderModel> GetAllAsync(OrderFilterModel orderFilter);

        /// <summary>
        /// Create order
        /// </summary>
        public Task<BaseModel> CreateAsync(OrderModelItem modelItem);

        /// <summary>
        /// Create payment add add to order
        /// </summary>
        public Task<BaseModel> UpdateAsync(PaymentModelItem modelItem);
    }
}
