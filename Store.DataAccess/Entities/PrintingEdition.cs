using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    [Table("PrintingEditions")]
    public partial class PrintingEdition : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; } = 0;
        public DateTime? Date { get; set; }
        public Enums.Enums.PrintingEditions.Currencies? Currency { get; set; }
        public Enums.Enums.PrintingEditions.Types? Type { get; set; }

        public virtual ICollection<AuthorInPrintingEdition> AuthorInBooks { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
