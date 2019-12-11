using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IAuthorInPrintingEditionRepository
    {
        public Task<bool> RemoveAuthorsInPrintingEditions(long printingEditionId);
    }
}
