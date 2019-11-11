using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    [Table("Orders", Schema = "dbo")]

    public class Orders
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int PaymentId { get; set; }


        public Users User { get; set; }
        public Payments Payment { get; set; }
        public ICollection<OrderItems> OrderItems { get; set; }
    }
}
