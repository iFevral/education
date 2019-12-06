using Store.BusinessLogic.Models.Base;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.Filters
{
    public class OrderFilterModel : BaseFilterModel
    {
        public IList<int> Statuses { get; set; }
    }
}
