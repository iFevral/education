using System.Linq;
using System.Threading.Tasks;
using Store.DataAccess.Entities;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class AuthorRepository : EFBaseRepository<Authors>, IAuthorRepository
    {
        public AuthorRepository(ApplicationContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> RemovePrintingEditionsAsync(int authorId)
        {
            _db.RemoveRange(_db.AuthorInBooks.Where(aib => aib.AuthorId == authorId));
            var result = await _db.SaveChangesAsync();
            return result > 0;
        }
    }
}
