using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        /// <summary>
        /// Remove printing editions from AuthorInBooks
        /// </summary>
        public Task<bool> RemovePrintingEditionsAsync(int authorId);
    }
}
