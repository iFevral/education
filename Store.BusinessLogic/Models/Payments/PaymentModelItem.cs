using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Payments
{
    public class PaymentModelItem :BaseModel
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
    }
}
