using Store.BusinessLogic.Models.Orders;

namespace Store.BusinessLogic.Common.Mappers.Order
{
    public static partial class OrderMapperExtension
    {
        public static OrderModelItem MapToModel(this DataAccess.Entities.Order entity)
        {
            var model = new OrderModelItem();

            model.Id = entity.Id;
            model.Date = entity.Date;
            model.Status = entity.Status.ToString();
            model.Description = entity.Description;
            
            foreach(var item in entity.OrderItems)
            {
                model.OrderItems.Add(new OrderItemModelItem
                {
                    Id = item.Id,
                    Amount = item.Amount,
                });
            }
            return model;
        }
    }
}
