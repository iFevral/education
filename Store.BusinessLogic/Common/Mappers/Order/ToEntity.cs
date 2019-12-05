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

            entity.Status = (Enums.Orders.Statuses)Enum.Parse(typeof(Enums.Orders.Statuses), model.Status);

            entity.PaymentId = model.Payment.Id;

            entity.OrderItems = new List<OrderItem>();
            foreach (var orderItems in model.OrderItems)
            {
                entity.OrderItems.Add(new OrderItem
                {
                    Amount = orderItems.Amount,
                    PrintingEditionId = orderItems.PrintingEdition.Id
                });
            }

            return entity;
        }
    }
}
