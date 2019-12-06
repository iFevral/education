using System.Collections.Generic;
using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Filters
{
    public class UserFilterModel : BaseFilterModel //todo UserFilterModel
    {
        //todo use one SearchString
        public string SearchQuery { get; set; }

        public IList<bool> Statuses { get; set; } //todo enum
    }
}
