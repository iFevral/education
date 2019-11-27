using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IPrintingEditionRepository : IGenericRepository<PrintingEditions>
    {
        /// <summary>
        /// Remove authors from AuthorInBooks
        /// </summary>
        public void RemoveAuthors(int printingEditionId);
    }
}