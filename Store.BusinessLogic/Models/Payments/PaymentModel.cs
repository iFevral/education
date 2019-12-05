using Store.BusinessLogic.Models.Base;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.Payments
{
    public class PaymentModel : BaseModel
    {
        public IList<PaymentModelItem> Items = new List<PaymentModelItem>();
    }
}
