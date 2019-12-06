using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.PrintingEditions;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderItemModelItem : BaseModel
    {
        public long Id { get; set; }
        public int Amount { get; set; } = 1;

        public PrintingEditionModelItem PrintingEdition { get; set; }
    }
}