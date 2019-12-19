using Store.BusinessLogic.Common;
using Store.DataAccess.Entities.Enums;

namespace Store.BusinessLogic.Extensions.Currency
{
    public static partial class CurrencyExtension
    {

        public static decimal ConvertFromUSD(this decimal? value, Enums.PrintingEditions.Currency? currency)
        {
            value = value * Constants.ExchangeRates.fromUSD[(int)currency - 1];
            return value.GetValueOrDefault();
        }

        public static decimal ConvertFromUSD(this decimal value, Enums.PrintingEditions.Currency? currency)
        {
            value = value * Constants.ExchangeRates.fromUSD[(int)currency - 1];
            return value;
        }
    }
}
