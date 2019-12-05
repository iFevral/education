using System;
using System.Linq.Expressions;
using Store.DataAccess.Entities.Enums;

namespace Store.BusinessLogic.Models.Base
{
    public abstract class BaseFilterModel<T> where T : class
    {
        public string SortProperty { get; set; } = "Id";
        public int SortWay { get; set; } = (int)Enums.Filter.SortWays.asc; //todo enums
        public int StartIndex { get; set; } = 0;
        public int Quantity { get; set; } = 0;

        public virtual Expression<Func<T, bool>> Predicate { get; } 
    }
}
