
using Store.BusinessLogic.Models.PrintingEditions;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderItemModelItem
    {
        public int Id { get; set; }
        public int Amount { get; set; } = 1;
        public int Count { get; set; } = 0;

        public OrderModelItem Order { get; set; }
        public PrintingEditionModelItem PrintingEdition { get; set; }
    }
}