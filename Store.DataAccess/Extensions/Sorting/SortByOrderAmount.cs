using System.Linq;
using System.Collections.Generic;
using Store.DataAccess.Entities;

namespace Store.DataAccess.Extensions.Sorting
{
    public static partial class SortingExtension
    {
        public static IEnumerable<Order> SortByOrderAmount(this IEnumerable<Order> orders, bool isAscending)
        {
            orders = isAscending
                ? orders.OrderBy(order => order, new OrderAmountComparer())
                : orders.OrderByDescending(order => order, new OrderAmountComparer());

            return orders;
        }
    }
}