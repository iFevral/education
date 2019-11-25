using System;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderInputData
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public int? PaymentId { get; set; }
        public IList<OrderItemInputData> Items { get; set; } = new List<OrderItemInputData>();
    }
}
