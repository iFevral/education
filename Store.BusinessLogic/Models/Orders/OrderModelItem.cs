using System;
using System.Collections.Generic;
using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Models.Payments;
using Store.DataAccess.Entities.Enums;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderModelItem : BaseModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public Enums.Order.OrderStatus Status { get; set; } = Enums.Order.OrderStatus.Unpaid;
        public DateTime? Date { get; set; } = DateTime.Now;
        public decimal? OrderPrice { get; set; }
        public UserModelItem User { get; set; } = new UserModelItem();
        public PaymentModelItem Payment { get; set; }

        public IList<OrderItemModelItem> OrderItems { get; set; } = new List<OrderItemModelItem>();
    }
}
