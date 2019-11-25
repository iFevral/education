using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Payments;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.EFRepositories;
using Store.DataAccess.Repositories.EFRepository;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IOrderItemRepository _orderItemRepository;
        private IPaymentRepository _paymentsRepository;
        private IMapper _mapper;
        public OrderService(ApplicationContext db,
                            IMapper mapper)
        {
            _mapper = mapper;
            _orderRepository = new OrderRepository(db);
            _orderItemRepository = new OrderItemRepository(db);
            _paymentsRepository = new PaymentRepository(db);
        }

        public async Task<OrderModel> AddPaymentTransaction(int orderId, PaymentModelItem modelItem)
        {
            var order = await _orderRepository.FindByIdAsync(orderId);
            var orderModel = new OrderModel();
            if (order == null)
            {
                orderModel.Errors.Add("Order not found");
                return orderModel;
            }

            await _paymentsRepository.CreateAsync(new Payments { TransactionId = modelItem.TransactionId });
            order.PaymentId = _paymentsRepository.FindBy(p => p.TransactionId.Equals(modelItem.TransactionId)).Id;
            await _orderRepository.UpdateAsync(order);
            return orderModel;
        }

        public async Task<OrderModel> Create(OrderInputData modelItem)
        {
            var orderModel = new OrderModel();
            var order = _mapper.Map<Orders>(modelItem);

            order.OrderItems = new List<OrderItems>();
            foreach (var item in modelItem.Items)
                order.OrderItems.Add(_mapper.Map<OrderItems>(item));

            await _orderRepository.CreateAsync(order);
            return orderModel;
        }

        public async Task<OrderModel> Delete(int id)
        {
            var orderModel = new OrderModel();
            var order = await _orderRepository.FindByIdAsync(id);
            if(order == null)
            {
                orderModel.Errors.Add("Order not found");
                return orderModel;
            }

            await _orderRepository.RemoveAsync(order);
            return orderModel;
        }

        public async Task<OrderModel> FindById(int id)
        {
            var orderModel = new OrderModel();
            var order = await _orderRepository.FindByIdAsync(id);
            if (order == null)
            {
                orderModel.Errors.Add("Order not found");
                return orderModel;
            }

            orderModel.Orders.Add(_mapper.Map<OrderModelItem>(order));
            return orderModel;
        }

        public OrderModel FindById(int id, string username)
        {
            var orderModel = new OrderModel();
            var order = _orderRepository.FindBy(o => o.User.UserName.Equals(username) && o.Id == id);
            if (order == null)
            {
                orderModel.Errors.Add("Order not found");
                return orderModel;
            }

            orderModel.Orders.Add(_mapper.Map<OrderModelItem>(order));
            return orderModel;
        }

        public async Task<OrderModel> GetAll(OrderFilter orderFilter, int startIndex = 0, int quantity = 0)
        {
            IList<Orders> orders;
            Func<Orders, bool> predicate = (o => (orderFilter.Username == null || o.User.UserName == orderFilter.Username));
            var orderModel = new OrderModel();
            
            orders = quantity > 0 
                ? _orderRepository.Get(predicate, startIndex, quantity)
                : _orderRepository.GetAll(predicate);

            if (orders == null)
            {
                orderModel.Errors.Add("Orders not found");
                return orderModel;
            }

            foreach(var item in orders)
            {
                var order = _mapper.Map<OrderModelItem>(item);
                foreach(var orderitem in item.OrderItems)
                    order.OrderItems.Add(_mapper.Map<OrderItemModelItem>(orderitem));
                
                orderModel.Orders.Add(_mapper.Map<OrderModelItem>(order));
            }
            return orderModel;
        }

        public async Task<OrderModel> RemovePaymentTransaction(int orderId)
        {
            var order = await _orderRepository.FindByIdAsync(orderId);
            var orderModel = new OrderModel();
            if (order == null)
            {
                orderModel.Errors.Add("Order not found");
                return orderModel;
            }

            order.PaymentId = null;
            await _orderRepository.UpdateAsync(order);
            return orderModel;
        }

        public async Task<OrderModel> Update(int id, OrderInputData modelItem)
        {
            var order = await _orderRepository.FindByIdAsync(id);
            var orderModel = new OrderModel();
            if (order == null)
            {
                orderModel.Errors.Add("Order not found");
                return orderModel;
            }

            _mapper.Map<OrderInputData, Orders>(modelItem, order);

            await _orderItemRepository.RemoveByOrderId(id);
            order.OrderItems = new List<OrderItems>();
            foreach (var item in modelItem.Items)
                order.OrderItems.Add(_mapper.Map<OrderItems>(item));

            await _orderRepository.UpdateAsync(order);
            return orderModel;
        }
    }
}
