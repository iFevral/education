using System;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Common.Mappers.Interface;
using Store.DataAccess.Entities;
using Store.DataAccess.Entities.Enums;
using System.Collections.Generic;

namespace Store.BusinessLogic.Common.Mappers
{
    public class OrderMapper : IMapper<Orders, OrderModelItem>
    {
        public Orders Map(OrderModelItem model, Orders entity)
        {
            entity.Description = string.IsNullOrWhiteSpace(model.Description)
                ? entity.Description
                : model.Description;
            entity.Status = string.IsNullOrWhiteSpace(model.Status)
                ? entity.Status
                : (Enums.Orders.Statuses)Enum.Parse(typeof(Enums.Orders.Statuses), model.Status);

            entity.UserId = model.User == null
                ? entity.UserId
                : model.User.Id;

            entity.PaymentId = model.Payment == null
                ? entity.PaymentId
                : model.Payment.Id;

            entity.OrderItems = new List<OrderItems>();
            foreach (var item in model.OrderItems)
            {
                entity.OrderItems.Add(new OrderItems
                {
                    Amount = item.Amount,
                    PrintingEditionId = item.PrintingEdition.Id
                });
            }

            return entity;
        }

        public OrderModelItem Map(Orders entity, OrderModelItem model)
        {
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
