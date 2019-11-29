using Store.BusinessLogic.Models.Base;
using System;
using System.Linq.Expressions;

namespace Store.BusinessLogic.Models.Authors
{
    public class AuthorFilter : BaseFilterModel
    {
        public string Name { get; set; }


        public Expression<Func<DataAccess.Entities.Authors, bool>> Predicate
        {
            get
            {
                return a => string.IsNullOrWhiteSpace(this.Name) || ValidateString(a.Name).Contains(ValidateString(this.Name));
            }
        }
    }
}
