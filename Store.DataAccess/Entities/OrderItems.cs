using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess
{
    public partial class OrderItems
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public int PrintingEditionId { get; set; }
        public int OrderId { get; set; }
        public int Count { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty(nameof(Orders.OrderItems))]
        public virtual Orders Order { get; set; }
        [ForeignKey(nameof(PrintingEditionId))]
        [InverseProperty(nameof(PrintingEditions.OrderItems))]
        public virtual PrintingEditions PrintingEdition { get; set; }
    }
}
