using Store.DataAccess.Entities;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class AuthorRepository : EFBaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
