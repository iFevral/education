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
using Store.DataAccess.Entities.Enums;

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

        public async Task<OrderModel> GetAllAsync(OrderFilterModel orderFilterModel)
        {
            IEnumerable<Order> orders;
            var orderModel = new OrderModel();
            var filterModel = orderFilterModel.MapToEFFilterModel();

            int counter = 0;

            orders = filterModel.SortProperty == Enums.Filter.SortProperty.Amount
                ? _orderRepository.GetAllSortedByAmount(filterModel, out counter)
                : _orderRepository.GetAll(filterModel, out counter);

            if (orders == null)
            {
                orderModel.Errors.Add(Constants.Errors.NotFoundOrderError);
                return orderModel;
            }

            orderModel.Counter = counter;
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

            result = await _orderRepository.UpdateAsync(order);
            if (!result)
            {
                orderModel.Errors.Add(Constants.Errors.UpdateOrderError);
            }

            return orderModel;
        }
    }
}
