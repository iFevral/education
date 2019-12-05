using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.Filters;
using Store.BusinessLogic.Models.PrintingEditions;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        public Task<PrintingEditionModel> GetAll(PrintingEditionFilterModel peFilter);
        public Task<PrintingEditionModelItem> FindByIdAsync(int id);
        public Task<BaseModel> CreateAsync(PrintingEditionModelItem printingEditionItem);
        public Task<BaseModel> UpdateAsync(PrintingEditionModelItem printingEditionItem);
        public Task<BaseModel> DeleteAsync(int id);
    }
}