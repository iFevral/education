using Store.BusinessLogic.Models.PrintingEditions;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        public PrintingEditionModel GetAll(PrintingEditionFilter peFilter, int startIndex, int quantity);
        public Task<PrintingEditionModel> FindByIdAsync(int id);
        public Task<PrintingEditionModel> CreateAsync(PrintingEditionModelItem printingEditionItem);
        public Task<PrintingEditionModel> UpdateAsync(int id, PrintingEditionModelItem printingEditionItem);
        public Task<PrintingEditionModel> DeleteAsync(int id);
    }
}