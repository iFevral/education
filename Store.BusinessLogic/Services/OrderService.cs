using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Payments;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.EFRepository;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IMapper _mapper;
        public OrderService(ApplicationContext db,
                            IMapper mapper)
        {
            _mapper = mapper;
            _orderRepository = new OrderRepository(db);
        }

        public async Task<OrderModel> AddItemToOrder(OrderItemModelItem modelItem)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderModel> AddPaymentTransaction(PaymentModelItem modelItem)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderModel> Create(OrderModelItem modelItem)
        {
            var orderModel = new OrderModel();
            var order = _mapper.Map<Orders>(modelItem);

            order.OrderItems = new List<OrderItems>();
            foreach (var item in modelItem.OrderItems)
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

        public async Task<OrderModel> GetAll(OrderFilter orderFilter, int startIndex = -1, int quantity = -1)
        {
            Func<Orders, bool> predicate = (o => o.User.UserName == orderFilter.Username);
            var orderModel = new OrderModel();
            var orders = _orderRepository.Get(predicate,startIndex,quantity);
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

        public async Task<OrderModel> RemoveItemFromOrder(OrderItemModelItem modelItem)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderModel> RemovePaymentTransaction(PaymentModelItem modelItem)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderModel> Update(int id, OrderModelItem modelItem)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderModel> UpdateItemInOrder(OrderItemModelItem modelItem)
        {
            throw new NotImplementedException();
        }
    }
}
