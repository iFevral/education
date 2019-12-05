using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.PrintingEditions;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderItemModelItem : BaseModel
    {
        public int Id { get; set; }
        public int Amount { get; set; } = 1;

        public OrderModelItem Order { get; set; } //todo check and remove
        public PrintingEditionModelItem PrintingEdition { get; set; }
    }
}