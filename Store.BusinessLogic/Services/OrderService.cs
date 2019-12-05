using System.Threading.Tasks;
using System.Collections.Generic;
using Store.BusinessLogic.Common;
using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Filters;
using Store.BusinessLogic.Models.Payments;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Common.Mappers.Order;
using Store.BusinessLogic.Common.Mappers.Filter;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;

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

        public async Task<OrderModelItem> FindByIdAsync(int id)
        {
            var orderModel = new OrderModelItem();
            var order = await _orderRepository.FindByIdAsync(id);
            if (order == null)
            {
                orderModel.Errors.Add(Constants.Errors.NotFoundOrderError);
                return orderModel;
            }

            orderModel = order.MapToModel();
            return orderModel;
        }

        public async Task<OrderModel> GetAllAsync(OrderFilterModel orderFilter)
        {
            IEnumerable<Order> orders;
            var orderModel = new OrderModel();

            orders = await _orderRepository.GetAllAsync(orderFilter.MapToDataAccessModel());

            if (orders == null)
            {
                orderModel.Errors.Add(Constants.Errors.NotFoundOrderError);
                return orderModel;
            }

            foreach (var item in orders)
            {
                var orderItem = new OrderModelItem();
                orderModel.Items.Add(item.MapToModel());
            }

            return orderModel;
        }

        public async Task<BaseModel> CreateAsync(OrderModelItem modelItem)
        {
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


        public async Task<BaseModel> UpdateAsync(PaymentModelItem modelItem) //todo rename updateOrder
        {
            var order = await _orderRepository.FindByIdAsync(modelItem.OrderId);
            var orderModel = new OrderModelItem();
            if (order == null)
            {
                orderModel.Errors.Add(Constants.Errors.NotFoundOrderError);
                return orderModel;
            }

            var result = await _paymentsRepository.CreateAsync(new Payment { TransactionId = modelItem.TransactionId }); //todo optimize
            if (!result)
            {
                orderModel.Errors.Add(Constants.Errors.CreatePaymentError);
                return orderModel;
            }

            var payment = await _paymentsRepository.FindByAsync(p => p.TransactionId.Equals(modelItem.TransactionId));
            order.PaymentId = payment.Id;

            result = await _orderRepository.UpdateAsync(order);
            if (!result)
            {
                orderModel.Errors.Add(Constants.Errors.UpdateOrderError);
            }

            return orderModel;
        }
    }
}
