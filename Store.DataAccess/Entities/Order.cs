﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    [Table("Orders")]
    public class Order : BaseEntity
    {
        public string Description { get; set; }
        public Enums.Enums.Order.OrderStatus Status { get; set; }
        public long UserId { get; set; }
        public long? PaymentId { get; set; }

        public virtual Payment Payment { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
