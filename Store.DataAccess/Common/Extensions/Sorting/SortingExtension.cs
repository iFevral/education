using System.Linq;
using System.Collections.Generic;

namespace Store.DataAccess.Common.Extensions.Sorting
{
    public static partial class SortingExtension
    {
        public static IEnumerable<T> GetSortedEnumerable<T>(this IEnumerable<T> items, bool isAscending, string property) where T : class
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