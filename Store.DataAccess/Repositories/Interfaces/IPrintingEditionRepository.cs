using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IPrintingEditionRepository : IGenericRepository<PrintingEdition>
    {
        /// <summary>
        /// Remove authors from AuthorInBooks
        /// </summary>
        public Task<bool> RemoveAuthors(long printingEditionId);
    }
}