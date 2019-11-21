using Store.BusinessLogic.Models.PrintingEditions;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        public PrintingEditionModel GetAll(PrintingEditionFilter peFilter);
    }
}