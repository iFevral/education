using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    [Table("PrintingEditions", Schema = "dbo")]
    public class PrintingEditions
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsRemoved { get; set; }
        public string Status { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }


        public ICollection<AuthorInBooks> AuthorInBooks { get; set; }
        public ICollection<OrderItems> OrderItems { get; set; }

    }
}
