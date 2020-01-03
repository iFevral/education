using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Base;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        public Task<DataModel<Author>> GetAllAuthors(FilterModel<Author> filterModel);
    }
}
