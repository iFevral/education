using System.Collections.Generic;
using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Filters
{
    public class UserFilterModel : BaseFilterModel
    {
        public string SearchQuery { get; set; }

        public IList<bool> LockStatuses { get; set; }
    }
}
