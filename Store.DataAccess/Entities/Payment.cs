using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    [Table("Payments")]
    public partial class Payment : BaseEntity
    {
        public string TransactionId { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
