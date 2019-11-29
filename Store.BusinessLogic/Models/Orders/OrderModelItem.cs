using System;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Models.Payments;
using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Orders
{
    public class OrderModelItem : BaseModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now;
        public UserModelItem User { get; set; }
        public PaymentModelItem Payment { get; set; }
    }
}
