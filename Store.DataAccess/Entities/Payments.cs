using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    public partial class Payments : BaseEntity
    {
        [StringLength(256)]
        public string TransactionId { get; set; }

        [InverseProperty("Payment")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
