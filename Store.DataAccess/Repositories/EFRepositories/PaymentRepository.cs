using Store.DataAccess.Entities;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class PaymentRepository : EFBaseRepository<Payments>, IPaymentRepository
    {
        private readonly ApplicationContext _db;
        public PaymentRepository(ApplicationContext db) : base(db)
        {
            _db = db;
        }
    }
}
