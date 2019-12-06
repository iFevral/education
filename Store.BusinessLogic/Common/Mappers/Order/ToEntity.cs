using System;
using System.Collections.Generic;
using Store.BusinessLogic.Models.Orders;
using Store.DataAccess.Entities;
using Store.DataAccess.Entities.Enums;

namespace Store.BusinessLogic.Common.Mappers.Order
{
    public static partial class OrderMapperExtension
    {
        public static DataAccess.Entities.Order MapToEntity(this OrderModelItem model, DataAccess.Entities.Order entity)
        {
            entity.Description = model.Description;

            entity.Status = model.Status;

            if (model.Payment != null)
            {
                entity.PaymentId = model.Payment.Id;
            }

            entity.UserId = model.User.Id;

            entity.OrderItems = new List<OrderItem>();
            foreach (var item in model.OrderItems)
            {
                var orderItem = new OrderItem();
                orderItem .Amount = item.Amount;
                orderItem .PrintingEditionId = item.PrintingEdition.Id;
                entity.OrderItems.Add(orderItem);
            }

            return entity;
        }
    }
}
