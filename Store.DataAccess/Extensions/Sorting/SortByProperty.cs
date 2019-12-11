using System.Linq;
using System.Collections.Generic;

namespace Store.DataAccess.Extensions.Sorting
{
    public static partial class SortingExtension
    {
        public static IEnumerable<T> SortBy<T>(this IEnumerable<T> items, string property, bool isAscending) where T : class
        {
            if (!isAscending)
            {
                    items = items.OrderByDescending(x => x.GetType().GetProperty(property).GetValue(x,null));
                return items;
            }

            items = items.OrderBy(x => x.GetType().GetProperty(property).GetValue(x, null));
            return items;
        }
    }
}