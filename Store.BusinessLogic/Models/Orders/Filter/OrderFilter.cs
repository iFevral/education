using Store.BusinessLogic.Models.Base;
using System;
using System.Linq.Expressions;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderFilter : BaseFilterModel
    {
        public DateTime Date { get; set; }
        public string Username { get; set; }

        public Expression<Func<DataAccess.Entities.Orders, bool>> Predicate
        {
            get
            {
                return o => string.IsNullOrWhiteSpace(this.Username) || ValidateString(o.User.UserName).Contains(ValidateString(this.Username));
            }
        }
    }
}
