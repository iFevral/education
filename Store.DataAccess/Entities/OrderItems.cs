﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    public partial class OrderItems
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public int Amount { get; set; } = 1;
        public int PrintingEditionId { get; set; }
        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty(nameof(Orders.OrderItems))]
        public virtual Orders Order { get; set; }
        [ForeignKey(nameof(PrintingEditionId))]
        [InverseProperty(nameof(PrintingEditions.OrderItems))]
        public virtual PrintingEditions PrintingEdition { get; set; }
    }
}
