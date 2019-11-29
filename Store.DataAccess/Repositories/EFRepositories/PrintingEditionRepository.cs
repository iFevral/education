using System.Linq;
using Store.DataAccess.Entities;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class PrintingEditionRepository : EFBaseRepository<PrintingEditions>, IPrintingEditionRepository
    {
        public PrintingEditionRepository(ApplicationContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> RemoveAuthors(int printingEditionId)
        {
            _db.RemoveRange(_db.AuthorInBooks.Where(aib => aib.PrintingEditionId == printingEditionId));
            var result = await _db.SaveChangesAsync();
            return result > 0;
        }
    }
}