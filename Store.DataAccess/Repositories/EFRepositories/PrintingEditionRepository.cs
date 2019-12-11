using Store.DataAccess.Entities;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class PrintingEditionRepository : EFBaseRepository<PrintingEdition>, IPrintingEditionRepository
    {
        public PrintingEditionRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}