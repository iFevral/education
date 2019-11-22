using Store.BusinessLogic.Models.Orders;

namespace Store.BusinessLogic.Models.Payments
{
    public class PaymentModelItem
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public OrderModelItem Order { get; set; }
    }
}
