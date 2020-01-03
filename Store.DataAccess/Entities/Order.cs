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

        [Dapper.Contrib.Extensions.Computed]
        public virtual Payment Payment { get; set; }
        [Dapper.Contrib.Extensions.Computed]
        public virtual User User { get; set; }
        [Dapper.Contrib.Extensions.Computed]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
