using Store.DataAccess.Entities;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.Models.Filters;
using System.Threading.Tasks;
using Store.DataAccess.Models;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class AuthorRepository : EFBaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public async Task<ListModel<Author>> GetAllAuthors(FilterModel<Author> filterModel)
        {
            var list = new ListModel<Author>();
            
            list.Items = GetAll(filterModel, out int counter);
            list.Counter = counter;

            return list;
        }
    }
}
