using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IAuthorInPrintingEditionRepository : IGenericRepository<AuthorInPrintingEdition>
    {
        public Task<bool> RemoveByPrintingEditionAsync(long printingEditionId);
    }
}
