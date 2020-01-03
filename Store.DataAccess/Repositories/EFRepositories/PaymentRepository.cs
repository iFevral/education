using Store.DataAccess.Entities;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class PaymentRepository : EFBaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
