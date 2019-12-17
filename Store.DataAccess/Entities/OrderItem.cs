using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    [Table("OrderItems")]
    public class OrderItem : BaseEntity
    {
        public int Amount { get; set; } = 1;
        public long PrintingEditionId { get; set; }
        public long OrderId { get; set; }

        public virtual Order Order { get; set; }

        public virtual PrintingEdition PrintingEdition { get; set; }
    }
}
