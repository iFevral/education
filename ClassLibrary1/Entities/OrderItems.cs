using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    [Table("OrderItems", Schema = "dbo")]
    public class OrderItems
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public int PrintingEditionId { get; set; }
        public int OrderId { get; set; }
        public int Count { get; set; }


        public PrintingEditions PrintingEdition { get; set; }
        public Orders Order { get; set; }

    }
}
