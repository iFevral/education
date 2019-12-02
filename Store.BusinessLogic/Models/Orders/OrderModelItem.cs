using System;
using System.Collections.Generic;
using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Models.Payments;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderModelItem : BaseModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now;
        public UserModelItem User { get; set; }
        public PaymentModelItem Payment { get; set; }

        public IList<OrderItemModelItem> OrderItems { get; set; } = new List<OrderItemModelItem>();
    }
}
