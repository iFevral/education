using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IPrintingEditionRepository : IGenericRepository<PrintingEditions>
    {
        /// <summary>
        /// Remove authors from AuthorInBooks
        /// </summary>
        public Task<bool> RemoveAuthors(int printingEditionId);
    }
}