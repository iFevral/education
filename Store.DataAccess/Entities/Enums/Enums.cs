namespace Store.DataAccess.Entities.Enums
{
    public static class Enums
    {
        public static class PrintingEditions 
        {
            public enum Currencies
            {
                USD = 1,
                EUR = 2,
                GBP = 3,
                CHF = 4,
                JPY = 5,
                UAH = 6
            }

            public enum Types
            {
                Book = 1,
                Journal = 2,
                Newspaper = 3
            }
        }

        public static class Orders
        {
            public enum Statuses
            {
                Paid = 1,
                Unpaid = 2
            }
        }

        public static class Roles
        {
            public enum RoleNames
            {
                Admin = 1,
                Client = 2
            }
        }
    }
}
