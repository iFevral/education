using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    [Table("Payments", Schema = "dbo")]
    public class Payments
    {
        [Key]
        public int Id { get; set; }
        public string TransactionId { get; set; }


        public ICollection<Orders> Orders { get; set; }
    }
}
