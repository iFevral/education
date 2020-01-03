using System.Collections.Generic;
using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderModel : BaseModel
    {
        public IList<OrderModelItem> Items { get; set; }  = new List<OrderModelItem>();
        public int Counter { get; set; }
    }
}
