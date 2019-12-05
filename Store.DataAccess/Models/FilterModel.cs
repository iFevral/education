using System;
using System.Linq.Expressions;

namespace Store.DataAccess.Models
{
    public class FilterModel<T> where T : class
    {
        public string SortProperty { get; set; }
        public int SortWay { get; set; }
        public int StartIndex { get; set; }
        public int Quantity { get; set; }

        public virtual Expression<Func<T, bool>> Predicate { get; set; }
    }
}
