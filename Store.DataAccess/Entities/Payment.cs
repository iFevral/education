using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    [Table("Payments")]
    public class Payment : BaseEntity
    {
        public string TransactionId { get; set; }

        [Dapper.Contrib.Extensions.Computed]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
