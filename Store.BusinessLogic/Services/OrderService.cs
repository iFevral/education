using System.Threading.Tasks;
using Store.BusinessLogic.Common.Constants;
using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Filters;
using Store.BusinessLogic.Models.Payments;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Common.Mappers.Order;
using Store.BusinessLogic.Common.Mappers.Filter;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.Entities.Enums;
using System.Linq;
using Store.BusinessLogic.Common.Mappers.OrderItem;

namespace Store.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentsRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderService(IPaymentRepository paymentsRepository,
                            IOrderRepository orderRepository,
                            IOrderItemRepository orderItemRepository)
        {
            _paymentsRepository = paymentsRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<OrderModel> GetAllAsync(OrderFilterModel orderFilterModel)
        {
            var orderModel = new OrderModel();
            var filterModel = orderFilterModel.MapToEFFilterModel();

            var listOfOrders = await _orderRepository.GetAllOrders(filterModel);

            if (listOfOrders.Items == null)
            {
                orderModel.Errors.Add(Constants.Errors.NotFoundOrdersError);
                return orderModel;
            }

            orderModel.Counter = listOfOrders.Counter;

            foreach (var item in listOfOrders.Items)
            {
                var orderItem = new OrderModelItem();
                orderModel.Items.Add(item.MapToModel());
            }

            return orderModel;
        }

        public async Task<BaseModel> CreateAsync(OrderModelItem modelItem)
        {
            if(modelItem.OrderItems == null || !modelItem.OrderItems.Any())
            {
                modelItem.Errors.Add(Constants.Errors.CreateOrderError);
                return modelItem;
            }

            var order = new Order();
            order = modelItem.MapToEntity(order);
            var orderId = await _orderRepository.CreateAsync(order);

            if (orderId == 0)
            {
                modelItem.Errors.Add(Constants.Errors.CreateOrderError);
                return modelItem;
            }

            var orderItems = modelItem.OrderItems.MapToOrderItemsList(orderId);
            var result = await _orderItemRepository.CreateListAsync(orderItems);

            if (!result && orderItems.Count() > 0)
            {
                modelItem.Errors.Add(Constants.Errors.CreateOrderError);
            }

            return modelItem;
        }

        public async Task<BaseModel> UpdateAsync(PaymentModelItem modelItem)
        {
            var order = await _orderRepository.FindByIdAsync(modelItem.OrderId);
            var orderModel = new OrderModelItem();
            if (order == null)
            {
                orderModel.Errors.Add(Constants.Errors.NotFoundOrderError);
                return orderModel;
            }

            var payment = new Payment();
            payment.TransactionId = modelItem.TransactionId;

            var paymentId = await _paymentsRepository.CreateAsync(payment);

            if (paymentId == 0)
            {
                orderModel.Errors.Add(Constants.Errors.CreatePaymentError);
                return orderModel;
            }
        
            order.PaymentId = paymentId;
            order.Status = Enums.Order.OrderStatus.Paid;
            
            var result = await _orderRepository.UpdateAsync(order);
            if (!result)
            {
                orderModel.Errors.Add(Constants.Errors.UpdateOrderError);
            }

            return orderModel;
        }
    }
}
