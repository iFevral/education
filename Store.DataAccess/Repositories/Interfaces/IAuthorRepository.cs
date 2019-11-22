using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Authors>
    {
        public void RemovePrintingEditions(int authorId);
    }
}
