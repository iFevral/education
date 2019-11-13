using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
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
        [StringLength(256)]
        public string Title { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; } = 0;
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
