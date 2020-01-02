using Store.DataAccess.Entities;

namespace Store.DataAccess.Models
{
    public class OrderModel : Order 
    {
        public decimal OrderPrice { get; set; }
    }
}
