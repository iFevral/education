using System;
using System.Collections.Generic;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Models.Payments;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderModelItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public UserModelItem User { get; set; }
        public PaymentModelItem Payment { get; set; }
        public IList<OrderItemModelItem> OrderItems { get; set; } = new List<OrderItemModelItem>();
    }
}
