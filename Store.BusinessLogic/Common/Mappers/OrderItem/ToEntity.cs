using Store.BusinessLogic.Models.Orders;
using System.Collections.Generic;

namespace Store.BusinessLogic.Common.Mappers.OrderItem
{
    public static partial class OrderItemMapperExtension
    {
        public static IEnumerable<DataAccess.Entities.OrderItem> MapToOrderItemsList(this IList<OrderItemModelItem> items, long orderId)
        {
            var list = new List<DataAccess.Entities.OrderItem>();

            foreach (var item in items)
            {
                var orderItem = new DataAccess.Entities.OrderItem();
                
                orderItem.OrderId = orderId;
                orderItem.Amount = item.Amount;
                orderItem.PrintingEditionId = item.PrintingEdition.Id;
                
                list.Add(orderItem);
            }

            return list;
        }
    }
}
