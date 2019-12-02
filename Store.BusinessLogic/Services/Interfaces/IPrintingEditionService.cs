using Store.BusinessLogic.Models.PrintingEditions;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        public PrintingEditionModel GetAll(PrintingEditionFilter peFilter, int startIndex, int quantity);
        public Task<PrintingEditionModelItem> FindByIdAsync(int id);
        public Task<PrintingEditionModelItem> CreateAsync(PrintingEditionModelItem printingEditionItem);
        public Task<PrintingEditionModelItem> UpdateAsync(int id, PrintingEditionModelItem printingEditionItem);
        public Task<PrintingEditionModelItem> DeleteAsync(int id);
    }
}