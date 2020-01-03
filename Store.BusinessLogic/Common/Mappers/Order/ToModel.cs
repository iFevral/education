using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Models.Users;

namespace Store.BusinessLogic.Common.Mappers.Order
{
    public static partial class OrderMapperExtension
    {
        public static OrderModelItem MapToModel(this DataAccess.Models.OrderModel entity)
        {
            var model = new OrderModelItem();

            model.Id = entity.Id;
            model.Date = entity.CreationDate;
            model.Status = entity.Status;
            model.Description = entity.Description;
            
            model.User = new UserModelItem();
            model.User.Id = entity.UserId;
            model.User.FirstName = entity.User.FirstName;
            model.User.LastName = entity.User.LastName;
            model.User.Email = entity.User.Email;

            model.OrderPrice = entity.OrderPrice;

            foreach (var item in entity.OrderItems)
            {
                var orderItem = new OrderItemModelItem();
                orderItem.Id = item.Id;
                orderItem.Amount = item.Amount;
                
                orderItem.PrintingEdition = new PrintingEditionModelItem();
                orderItem.PrintingEdition.Title = item.PrintingEdition.Title;
                orderItem.PrintingEdition.Type = item.PrintingEdition.Type;
                orderItem.PrintingEdition.Price = item.PrintingEdition.Price;

                model.OrderItems.Add(orderItem);
            }
            return model;
        }
    }
}
