using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderFilter : BaseFilterModel
    {
        public DateTime Date { get; set; }
        public string Username { get; set; }
        public IList<int> Statuses { get; set; }

        public Expression<Func<DataAccess.Entities.Orders, bool>> Predicate
        {
            get
            {
                return o => (string.IsNullOrWhiteSpace(this.Username) || o.User.UserName.ToLower().Contains(this.Username.ToLower())) &&
                            (this.Statuses == null || this.Statuses.Count == 0 || this.Statuses.Any(s => s == (int)o.Status)) &&
                            (!o.isRemoved);
            }
        }
    }
}
