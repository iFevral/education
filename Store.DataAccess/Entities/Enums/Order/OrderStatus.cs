namespace Store.DataAccess.Entities.Enums
{
    public static partial class Enums
    {
        public static class Order
        {
            public enum OrderStatus
            {
                None = 0,
                Paid = 1,
                Unpaid = 2
            }
        }
    }
}
