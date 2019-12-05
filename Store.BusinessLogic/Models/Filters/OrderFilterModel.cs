using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Filters
{
    public class OrderFilterModel : BaseFilterModel<DataAccess.Entities.Order>
    {
        public IList<int> Statuses { get; set; }

        public override Expression<Func<DataAccess.Entities.Order, bool>> Predicate
        {
            get
            {
                return order => (this.Statuses == null || this.Statuses.Count == 0 || this.Statuses.Any(s => s == (int)order.Status)) &&
                            (!order.isRemoved);
            }
        }
    }
}
