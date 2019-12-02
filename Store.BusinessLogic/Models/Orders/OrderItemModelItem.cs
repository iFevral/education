using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.PrintingEditions;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderItemModelItem : BaseModel
    {
        public int Id { get; set; }
        public int Amount { get; set; } = 1;
        public string PrintingEditionTitle { get; set; }

        public OrderModelItem Order { get; set; }
        public PrintingEditionModelItem PrintingEdition { get; set; }
    }
}