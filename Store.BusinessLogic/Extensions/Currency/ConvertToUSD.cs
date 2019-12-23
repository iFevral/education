using Store.BusinessLogic.Common.Constants;
using Store.DataAccess.Entities.Enums;

namespace Store.BusinessLogic.Extensions.Currency
{
    public static partial class CurrencyExtension
    {
        public static decimal ConvertToUSD(this decimal? value, Enums.PrintingEditions.Currency? currency )
        {
            value = value * Constants.ExchangeRates.toUSD[(int)currency - 1];
            return value.GetValueOrDefault();
        }

        public static decimal ConvertToUSD(this decimal value, Enums.PrintingEditions.Currency? currency)
        {
            value = value * Constants.ExchangeRates.toUSD[(int)currency - 1];
            return value;
        }
    }
}
