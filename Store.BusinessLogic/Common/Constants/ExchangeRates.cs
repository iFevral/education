using Store.DataAccess.Entities.Enums;
using System.Collections.Generic;

namespace Store.BusinessLogic.Common.Constants
{
    public static partial class Constants
    {
        public class ExchangeRates
        {
            public static readonly Dictionary<Enums.PrintingEditions.Currency, decimal> fromUSD = new Dictionary<Enums.PrintingEditions.Currency, decimal>()
            {
                { Enums.PrintingEditions.Currency.USD, 1m },
                { Enums.PrintingEditions.Currency.EUR, 0.9009m },
                { Enums.PrintingEditions.Currency.GBP, 0.770772m },
                { Enums.PrintingEditions.Currency.CHF, 0.9811m },
                { Enums.PrintingEditions.Currency.JPY, 109.385m },
                { Enums.PrintingEditions.Currency.UAH, 23.245m },
            };

            public static readonly Dictionary<Enums.PrintingEditions.Currency, decimal> toUSD = new Dictionary<Enums.PrintingEditions.Currency, decimal>()
            {
                { Enums.PrintingEditions.Currency.USD, 1m },
                { Enums.PrintingEditions.Currency.EUR, 1.11m },
                { Enums.PrintingEditions.Currency.GBP, 1.2974m },
                { Enums.PrintingEditions.Currency.CHF, 1.01926m },
                { Enums.PrintingEditions.Currency.JPY, 0.00914202m },
                { Enums.PrintingEditions.Currency.UAH, 0.04302m },
            };
        }
    }
}
