using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    [Table("OrderItems")]
    public partial class OrderItem : BaseEntity
    {
        public int Amount { get; set; } = 1;
        public int PrintingEditionId { get; set; }
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public virtual PrintingEdition PrintingEdition { get; set; }
    }
}
