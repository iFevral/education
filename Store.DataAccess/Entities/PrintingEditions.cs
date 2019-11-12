using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess
{
    public partial class PrintingEditions
    {
        public PrintingEditions()
        {
            AuthorInBooks = new HashSet<AuthorInBooks>();
            OrderItems = new HashSet<OrderItems>();
        }

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public bool IsRemoved { get; set; }
        public string Status { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }

        [InverseProperty("PrintingEdition")]
        public virtual ICollection<AuthorInBooks> AuthorInBooks { get; set; }
        [InverseProperty("PrintingEdition")]
        public virtual ICollection<OrderItems> OrderItems { get; set; }
    }
}
