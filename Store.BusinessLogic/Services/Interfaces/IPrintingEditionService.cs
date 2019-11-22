using Store.BusinessLogic.Models.PrintingEditions;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        public PrintingEditionModel GetAll(PrintingEditionFilter peFilter, int startIndex, int quantity);
        public Task<PrintingEditionModel> FindById(int id);
        public Task<PrintingEditionModel> Create(PrintingEditionModelItem printingEditionItem);
        public Task<PrintingEditionModel> Update(int id, PrintingEditionModelItem printingEditionItem);
        public Task<PrintingEditionModel> Delete(int id);
    }
}