using Store.DataAccess.Entities.Enums;
using System;
using System.Linq.Expressions;

namespace Store.DataAccess.Models.EFFilters
{
    public class EFFilterModel<T> where T : class
    {
        public Enums.Filter.SortProperties SortProperty { get; set; }
        public bool IsAscending { get; set; }
        public int StartIndex { get; set; }
        public int Quantity { get; set; }

        public virtual Expression<Func<T, bool>> Predicate { get; set; }
    }
}
