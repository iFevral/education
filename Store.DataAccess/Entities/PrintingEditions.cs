using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    public partial class PrintingEditions : BaseEntity
    {
        [StringLength(256)]
        public string Title { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; } = 0;
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }
        [StringLength(10)]
        public Enums.Enums.PrintingEditions.Currencies? Currency { get; set; }
        [StringLength(20)]
        public Enums.Enums.PrintingEditions.Types? Type { get; set; }

        [InverseProperty("PrintingEdition")]
        public virtual ICollection<AuthorInBooks> AuthorInBooks { get; set; }
        [InverseProperty("PrintingEdition")]
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
