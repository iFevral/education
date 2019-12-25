namespace Store.BusinessLogic.Common.Constants
{
    public static partial class Constants
    {
        public class ExchangeRates
        {
            public static readonly decimal[] fromUSD = 
            {
                1m,      //USD
                0.9009m,    //EUR
                0.770772m,   //GBP
                0.9811m,   //CHF
                109.385m, //JPY
                23.245m,  //UAH
            };

            public static readonly decimal[] toUSD = 
            {
                1,      //USD
                1.11m,   //EUR
                1.2974m,   //GBP
                1.01926m,   //CHF
                0.00914202m, //JPY
                0.04302m   //UAH
            };
        }
    }
}
