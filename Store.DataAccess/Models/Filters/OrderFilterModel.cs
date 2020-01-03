using System.Collections.Generic;
using Store.DataAccess.Entities;

namespace Store.DataAccess.Models.Filters
{
    public class OrderFilterModel : FilterModel<Order>
    {
        public long? UserId { get; set; }
        public IList<int> Statuses { get; set; }
    }
}
