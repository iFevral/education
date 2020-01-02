using System.Threading.Tasks;
using System.Collections.Generic;
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

namespace Store.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentsRepository;
        public OrderService(IPaymentRepository paymentsRepository,
                            IOrderRepository orderRepository)
        {
            _paymentsRepository = paymentsRepository;
            _orderRepository = orderRepository;
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

            var result = await _orderRepository.CreateAsync(order);

            if (!result)
            {
                modelItem.Errors.Add(Constants.Errors.CreateOrderError);
            }

            return modelItem;
        }

        public async Task<BaseModel> DeleteAsync(int id)
        {
            var order = await _orderRepository.FindByIdAsync(id);
            var orderModel = new OrderModelItem();
            if(order == null)
            {
                orderModel.Errors.Add(Constants.Errors.NotFoundOrderError);
                return orderModel;
            }

            order.isRemoved = true;
            var result = await _orderRepository.UpdateAsync(order);
            if (!result)
            {
                orderModel.Errors.Add(Constants.Errors.DeleteOrderError);
                return orderModel;
            }

            return orderModel;
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

            var result = await _paymentsRepository.CreateAsync(payment);
            if (!result)
            {
                orderModel.Errors.Add(Constants.Errors.CreatePaymentError);
                return orderModel;
            }
        
            payment = await _paymentsRepository.FindAsync(p => p.TransactionId.Equals(modelItem.TransactionId));
            order.PaymentId = payment.Id;
            order.Status = Enums.Order.OrderStatus.Paid;
            result = await _orderRepository.UpdateAsync(order);
            if (!result)
            {
                orderModel.Errors.Add(Constants.Errors.UpdateOrderError);
            }

            return orderModel;
        }
    }
}
