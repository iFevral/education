using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    [Table("Orders")]
    public partial class Order : BaseEntity
    {
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public Enums.Enums.Orders.Statuses? Status { get; set; }
        public string UserId { get; set; }
        public int? PaymentId { get; set; }

        public virtual Payment Payment { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
