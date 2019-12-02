using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Payments;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IOrderItemRepository _orderItemRepository;
        private IPaymentRepository _paymentsRepository;
        private IMapper _mapper;
        public OrderService(IMapper mapper,
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
                orderModel.Errors.Add("Order not found");
                return orderModel;
            }

            var result = await _paymentsRepository.CreateAsync(new Payments { TransactionId = modelItem.TransactionId });
            if(!result)
            {
                orderModel.Errors.Add("Creating payment error");
                return orderModel;
            }

            order.PaymentId = _paymentsRepository.FindBy(p => p.TransactionId.Equals(modelItem.TransactionId)).Id;
            
            result = await _orderRepository.UpdateAsync(order);
            if (!result)
            {
                orderModel.Errors.Add("Order not found");
                return orderModel;
            }

            return orderModel;
        }

        public async Task<OrderModelItem> CreateAsync(OrderModelItem modelItem)
        {
            var orderModel = new OrderModelItem();
            var order = _mapper.Map<Orders>(modelItem);

            order.OrderItems = new List<OrderItems>();

            var result = await _orderRepository.CreateAsync(order);
            if (!result)
            {
                orderModel.Errors.Add("Creating order error");
                return orderModel;
            }

            return orderModel;
        }

        public async Task<OrderModelItem> DeleteAsync(int id)
        {
            var order = await _orderRepository.FindByIdAsync(id);
            var orderModel = new OrderModelItem();
            if(order == null)
            {
                orderModel.Errors.Add("Order not found");
                return orderModel;
            }

            order.isRemoved = true;
            var result = await _orderRepository.UpdateAsync(order);
            if (!result)
            {
                orderModel.Errors.Add("Creating order error");
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
                orderModel.Errors.Add("Order not found");
                return orderModel;
            }

            orderModel = _mapper.Map<OrderModelItem>(order);
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
                orderModel.Errors.Add("Orders not found");
                return orderModel;
            }

            foreach(var item in orders)
            {
                var order = _mapper.Map<OrderModelItem>(item);
                orderModel.Orders.Add(_mapper.Map<OrderModelItem>(order));
            }

            return orderModel;
        }

        public async Task<OrderModelItem> RemovePaymentTransactionAsync(int orderId)
        {
            var order = await _orderRepository.FindByIdAsync(orderId);
            var orderModel = new OrderModelItem();
            if (order == null)
            {
                orderModel.Errors.Add("Order not found");
                return orderModel;
            }

            order.PaymentId = null;
            var result = await _orderRepository.UpdateAsync(order);
            if (!result)
            {
                orderModel.Errors.Add("Removing payment error");
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
                orderModel.Errors.Add("Order not found");
                return orderModel;
            }

            _mapper.Map<OrderModelItem, Orders>(modelItem, order);

            var result = await _orderRepository.UpdateAsync(order);
            if(!result)
            {
                orderModel.Errors.Add("Updating order error");
                return orderModel;
            }
            return orderModel;
        }
    }
}
