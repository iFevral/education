﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    public partial class PrintingEditions
    {
        [Key]
        public int Id { get; set; }
        [StringLength(256)]
        public string Title { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; } = 0;
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }
        public bool IsRemoved { get; set; } = false;
        [StringLength(20)]
        public string Status { get; set; }
        [StringLength(10)]
        public string Currency { get; set; }
        [StringLength(20)]
        public string Type { get; set; }

        [InverseProperty("PrintingEdition")]
        public virtual ICollection<AuthorInBooks> AuthorInBooks { get; set; }
        [InverseProperty("PrintingEdition")]
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
