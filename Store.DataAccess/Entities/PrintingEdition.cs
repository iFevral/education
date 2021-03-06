﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    [Table("PrintingEditions")]
    public class PrintingEdition : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; } = 0;
        public DateTime? Date { get; set; }
        public Enums.Enums.PrintingEditions.Currency? Currency { get; set; }
        public Enums.Enums.PrintingEditions.PrintingEditionType? Type { get; set; }
        public string Image { get; set; }

        [Dapper.Contrib.Extensions.Computed]
        public virtual ICollection<AuthorInPrintingEdition> AuthorInPrintingEditions { get; set; } = new List<AuthorInPrintingEdition>();
        [Dapper.Contrib.Extensions.Computed]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
