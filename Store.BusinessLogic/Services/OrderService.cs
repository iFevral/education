using System.Threading.Tasks;
using System.Collections.Generic;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Payments;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.BusinessLogic.Common.Mappers.Interface;
using Store.BusinessLogic.Common;

namespace Store.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IPaymentRepository _paymentsRepository;
        private readonly IMapper<Orders, OrderModelItem> _mapper;
        public OrderService(IMapper<Orders, OrderModelItem> mapper,
                            IPaymentRepository paymentsRepository,
                            IOrderRepository orderRepository,
                            IOrderItemRepository orderItemRepository)
        {
            _mapper = mapper;
            _paymentsRepository = paymentsRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<OrderModelItem> AddPaymentTransactionAsync(int orderId, PaymentModelItem modelItem)
        {
            var order = await _orderRepository.FindByIdAsync(orderId);
            var orderModel = new OrderModelItem();
            if (order == null)
            {
                orderModel.Errors.Add(Constants.Errors.NotFoundOrderError);
                return orderModel;
            }

            var result = await _paymentsRepository.CreateAsync(new Payments { TransactionId = modelItem.TransactionId });
            if(!result)
            {
                orderModel.Errors.Add(Constants.Errors.CreatePaymentError);
                return orderModel;
            }

            order.PaymentId = _paymentsRepository.FindBy(p => p.TransactionId.Equals(modelItem.TransactionId)).Id;
            
            result = await _orderRepository.UpdateAsync(order);
            if (!result)
            {
                orderModel.Errors.Add(Constants.Errors.UpdateOrderError);
                return orderModel;
            }

            return orderModel;
        }

        public async Task<OrderModelItem> CreateAsync(OrderModelItem modelItem)
        {
            var order = new Orders();
            order = _mapper.Map(modelItem, order);

            var result = await _orderRepository.CreateAsync(order);
            if (!result)
            {
                modelItem.Errors.Add(Constants.Errors.CreateOrderError);
                return modelItem;
            }

            return modelItem;
        }

        public async Task<OrderModelItem> DeleteAsync(int id)
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

        public async Task<OrderModelItem> FindByIdAsync(int id)
        {
            var orderModel = new OrderModelItem();
            var order = await _orderRepository.FindByIdAsync(id);
            if (order == null)
            {
                orderModel.Errors.Add(Constants.Errors.NotFoundOrderError);
                return orderModel;
            }

            orderModel = _mapper.Map(order, orderModel);
            return orderModel;
        }

        public async Task<OrderModel> GetAllAsync(OrderFilter orderFilter, int startIndex = 0, int quantity = 0)
        {
            IList<Orders> orders;
            var orderModel = new OrderModel();
            
            orders = quantity > 0 
                ? _orderRepository.Get(orderFilter.Predicate, startIndex, quantity)
                : _orderRepository.GetAll(orderFilter.Predicate);

            if (orders == null)
            {
                orderModel.Errors.Add(Constants.Errors.NotFoundOrderError);
                return orderModel;
            }

            foreach(var item in orders)
            {
                var orderItem = new OrderModelItem();
                orderItem = _mapper.Map(item, orderItem);
                orderModel.Orders.Add(orderItem);
            }

            return orderModel;
        }

        public async Task<OrderModelItem> RemovePaymentTransactionAsync(int orderId)
        {
            var order = await _orderRepository.FindByIdAsync(orderId);
            var orderModel = new OrderModelItem();
            if (order == null)
            {
                orderModel.Errors.Add(Constants.Errors.NotFoundOrderError);
                return orderModel;
            }

            order.PaymentId = null;
            var result = await _orderRepository.UpdateAsync(order);
            if (!result)
            {
                orderModel.Errors.Add(Constants.Errors.RemovePaymentError);
                return orderModel;
            }

            return orderModel;
        }

        public async Task<OrderModelItem> UpdateAsync(int id, OrderModelItem modelItem)
        {
            var order = await _orderRepository.FindByIdAsync(id);
            var orderModel = new OrderModelItem();
            if (order == null)
            {
                orderModel.Errors.Add(Constants.Errors.NotFoundOrderError);
                return orderModel;
            }
            await _orderItemRepository.RemoveByOrderIdAsync(order.Id);
            order = _mapper.Map(modelItem, order);
            var result = await _orderRepository.UpdateAsync(order);
            if(!result)
            {
                orderModel.Errors.Add(Constants.Errors.UpdateOrderError);
                return orderModel;
            }
            return orderModel;
        }
    }
}
