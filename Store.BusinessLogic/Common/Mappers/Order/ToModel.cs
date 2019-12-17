using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Models.Users;
using System.Linq;

namespace Store.BusinessLogic.Common.Mappers.Order
{
    public static partial class OrderMapperExtension
    {
        public static OrderModelItem MapToModel(this DataAccess.Entities.Order entity)
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

            var query = entity.OrderItems
                .GroupBy(item => item.OrderId)
                .Select(g => new 
                             { 
                                 OrderId = g.Key,
                                 Amount = g.Sum(item => item.PrintingEdition.Price * item.Amount) 
                             });

            model.OrderPrice = query.First().Amount;

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
