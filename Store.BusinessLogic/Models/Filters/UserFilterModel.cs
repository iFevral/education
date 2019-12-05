using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Filters
{
    public class UserFilterModel : BaseFilterModel<DataAccess.Entities.User> //todo UserFilterModel
    {
        //todo use one SearchString
        public string SearchQuery { get; set; }

        public IList<bool> Statuses { get; set; } //todo enum

        public override Expression<Func<DataAccess.Entities.User, bool>> Predicate
        {
            get
            {
                var userName = Regex.Split(this.SearchQuery, @"\s+");
                string firstName = userName[0],
                       lastName = userName[1];

                return user => (string.IsNullOrWhiteSpace(firstName) || user.FirstName.ToLower().Contains(firstName.ToLower())) &&
                               (string.IsNullOrWhiteSpace(lastName) || user.LastName.ToLower().Contains(lastName.ToLower())) &&
                               (this.Statuses != null || this.Statuses.Count == 0 || this.Statuses.Any(s => s == user.LockoutEnabled)) &&
                               (!user.isRemoved);

            }
        }
    }
}
