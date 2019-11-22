using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IPrintingEditionRepository : IGenericRepository<PrintingEditions>
    {
        public void RemoveAuthors(int printingEditionId);
    }
}