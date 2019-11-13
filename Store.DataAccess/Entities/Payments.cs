﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    public partial class Payments
    {
        public Payments()
        {
            Orders = new HashSet<Orders>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(256)]
        public string TransactionId { get; set; }

        [InverseProperty("Payment")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
