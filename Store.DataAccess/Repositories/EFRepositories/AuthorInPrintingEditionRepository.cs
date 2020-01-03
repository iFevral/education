using System.Linq;
using System.Threading.Tasks;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class AuthorInPrintingEditionRepository : EFBaseRepository<AuthorInPrintingEdition>, IAuthorInPrintingEditionRepository
    {
        public AuthorInPrintingEditionRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbSet = _dbContext.AuthorInPrintingEditions;
        }

        public async Task<bool> RemoveByPrintingEditionAsync(long printingEditionId)
        {
            var authorInPrintingEdition = _dbSet.Where(aib => aib.PrintingEditionId == printingEditionId);

            if(authorInPrintingEdition == null || !authorInPrintingEdition.Any())
            {
                return true;
            }

            _dbContext.AuthorInPrintingEditions.RemoveRange(authorInPrintingEdition);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
