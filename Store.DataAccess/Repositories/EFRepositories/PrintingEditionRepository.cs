using System.Linq;
using Store.DataAccess.Entities;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class PrintingEditionRepository : EFBaseRepository<PrintingEdition>, IPrintingEditionRepository
    {
        public PrintingEditionRepository(ApplicationContext db) : base(db)
        {
            _dbContext = db;
        }

        public async Task<bool> RemoveAuthors(long printingEditionId)
        {
            _dbContext.RemoveRange(_dbContext.AuthorInBooks.Where(aib => aib.PrintingEditionId == printingEditionId));
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}