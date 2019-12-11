using Store.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Store.DataAccess.Extensions
{
    public class OrderAmountComparer : IComparer<Order>
    {
        public int Compare(Order x, Order y)
        {
            var xAmount = GetOrderAmount(x);
            var yAmount = GetOrderAmount(y);

            if (xAmount > yAmount) return 1;
            else if (xAmount < yAmount) return -1;
            else return 0;
        }

        private decimal GetOrderAmount(Order item)
        {
            var query = item.OrderItems
                .GroupBy(item => item.OrderId)
                .Select(g => new
                {
                    OrderId = g.Key,
                    Amount = g.Sum(item => item.PrintingEdition.Price * item.Amount)
                });

            if (query.Any())
            {
                var amount = query.First().Amount;
                return amount;
            }

            return 0;
        }
    }
}
