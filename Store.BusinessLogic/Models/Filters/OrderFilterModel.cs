using Store.BusinessLogic.Models.Base;
using Store.DataAccess.Entities.Enums;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.Filters
{
    public class OrderFilterModel : BaseFilterModel
    {
        public long? UserId { get; set; }
        public IList<int> Statuses { get; set; }
    }
}
