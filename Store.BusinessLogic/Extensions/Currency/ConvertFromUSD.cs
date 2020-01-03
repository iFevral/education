using Store.BusinessLogic.Common.Constants;
using Store.DataAccess.Entities.Enums;

namespace Store.BusinessLogic.Extensions.Currency
{
    public static partial class CurrencyExtension
    {

        public static decimal ConvertFromUSD(this decimal? value, Enums.PrintingEditions.Currency currency)
        {
            value = value * Constants.ExchangeRates.fromUSD[currency];
            return value.GetValueOrDefault();
        }

        public static decimal ConvertFromUSD(this decimal value, Enums.PrintingEditions.Currency currency)
        {
            value = value * Constants.ExchangeRates.fromUSD[currency];
            return value;
        }
    }
}
