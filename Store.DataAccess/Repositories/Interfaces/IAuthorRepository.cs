using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Authors>
    {
        /// <summary>
        /// Remove printing editions from AuthorInBooks
        /// </summary>
        public void RemovePrintingEditions(int authorId);
    }
}
