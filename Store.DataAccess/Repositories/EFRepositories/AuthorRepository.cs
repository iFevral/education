using System.Linq;
using System.Threading.Tasks;
using Store.DataAccess.Entities;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class AuthorRepository : EFBaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationContext db) : base(db)
        {
            _dbContext = db;
        }

        public async Task<bool> RemovePrintingEditionsAsync(int authorId)
        {
            _dbContext.RemoveRange(_dbContext.AuthorInBooks.Where(aib => aib.AuthorId == authorId));
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
