using Store.DataAccess.Entities;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.Models.Filters;
using System.Threading.Tasks;
using Store.DataAccess.Models;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class PrintingEditionRepository : EFBaseRepository<PrintingEdition>, IPrintingEditionRepository
    {
        public PrintingEditionRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<DataModel<PrintingEdition>> GetAllPrintingEditions(PrintingEditionFilterModel filterModel)
        {
            var list = new DataModel<PrintingEdition>();

            list.Items = GetAll(filterModel, out int counter);
            list.Counter = counter;

            return list;
        }
    }
}