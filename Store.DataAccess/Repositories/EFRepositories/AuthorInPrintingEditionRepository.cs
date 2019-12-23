using System.Linq;
using System.Threading.Tasks;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class AuthorInPrintingEditionRepository : IAuthorInPrintingEditionRepository
    {
        private readonly ApplicationContext _dbContext;
        public AuthorInPrintingEditionRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> RemoveAuthorsInPrintingEditions(long printingEditionId)
        {
            var authorInPrintingEdition = _dbContext.AuthorInBooks.Where(aib => aib.PrintingEditionId == printingEditionId);

            if(!authorInPrintingEdition.Any())
            {
                return true;
            }

            _dbContext.AuthorInBooks.RemoveRange(authorInPrintingEdition);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
