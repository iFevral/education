namespace Store.BusinessLogic.Common.Constants
{
    public static partial class Constants
    {
        public class ExchangeRates
        {
            public static readonly decimal[] fromUSD = 
            {
                1m,      //USD
                0.9m,    //EUR
                0.77m,   //GBP
                0.98m,   //CHF
                109.41m, //JPY
                23.36m,  //UAH
            };

            public static readonly decimal[] toUSD = 
            {
                1,      //USD
                1.11m,   //EUR
                1.30m,   //GBP
                1.02m,   //CHF
                0.0091m, //JPY
                0.043m   //UAH
            };
        }
    }
}
