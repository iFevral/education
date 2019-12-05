using System.Threading.Tasks;
using System.Collections.Generic;
using Store.BusinessLogic.Common;
using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.Filters;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Common.Mappers.Filter;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Common.Mappers.PrintingEdition;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.BusinessLogic.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {

        private readonly IPrintingEditionRepository _printingEditionRepository;

        public PrintingEditionService(IPrintingEditionRepository printingEditionRepository)
        {
            _printingEditionRepository = printingEditionRepository;
        }

        public async Task<int> GetNumberOfPrintingEditions()
        {
            return await _printingEditionRepository.GetNumberOfItems();
        }

        public async Task<PrintingEditionModel> GetAll(PrintingEditionFilterModel printingEditionFilter)
        {
            var printingEditionModel = new PrintingEditionModel();

            IEnumerable<PrintingEdition> printingEditions;
            
            printingEditions = await _printingEditionRepository.GetAllAsync(printingEditionFilter.MapToDataAccessModel());
            

            if (printingEditions == null)
            {
                printingEditionModel.Errors.Add(Constants.Errors.NotFoundPringtingEditionError);
                return printingEditionModel;
            }

            foreach( var printingEdition in printingEditions)
            {
                var peModelItem = new PrintingEditionModelItem();
                printingEditionModel.Items.Add(printingEdition.MapToModel());
            }

            return printingEditionModel;
        }

        public async Task<PrintingEditionModelItem> FindByIdAsync(int id)
        {
            var printingEditionModel = new PrintingEditionModelItem();
            var printingEdition = await _printingEditionRepository.FindByIdAsync(id);
            if (printingEdition == null)
            {
                printingEditionModel.Errors.Add(Constants.Errors.NotFoundPringtingEditionError);
                return printingEditionModel;
            }

            printingEditionModel = printingEdition.MapToModel();
           
            return printingEditionModel;
        }

        public async Task<BaseModel> CreateAsync(PrintingEditionModelItem printingEditionModel)
        {
            var printingEdition = new PrintingEdition();
            printingEdition = printingEditionModel.MapToEntity(printingEdition);
           
            var result = await _printingEditionRepository.CreateAsync(printingEdition);
            if (!result)
            {
                printingEditionModel.Errors.Add(Constants.Errors.CreatePringtingEditionError);
            }

            return printingEditionModel;
        }

        public async Task<BaseModel> UpdateAsync(PrintingEditionModelItem printingEditionModel) //todo add authors
        {
            var printingEdition = await _printingEditionRepository.FindByIdAsync(printingEditionModel.Id);
            if(printingEdition == null)
            {
                printingEditionModel.Errors.Add(Constants.Errors.NotFoundPringtingEditionError);
                return printingEditionModel;
            }

            printingEdition = printingEditionModel.MapToEntity(printingEdition);
            
            var result = await _printingEditionRepository.RemoveAuthors(printingEdition.Id);
            if (!result)
            {
                printingEditionModel.Errors.Add(Constants.Errors.DeleteAuthorError);
                return printingEditionModel;
            }

            result = await _printingEditionRepository.UpdateAsync(printingEdition);
            if(!result)
            {
                printingEditionModel.Errors.Add(Constants.Errors.UpdatePringtingEditionError);
            }

            return printingEditionModel;
        }

        public async Task<BaseModel> DeleteAsync(int id)
        {
            var printingEditionModel = new PrintingEditionModelItem();
            var printingEdition = await _printingEditionRepository.FindByIdAsync(id);
            if (printingEdition == null)
            {
                printingEditionModel.Errors.Add(Constants.Errors.NotFoundPringtingEditionError);
                return printingEditionModel;
            }

            printingEdition.isRemoved = true;
            var result = await _printingEditionRepository.UpdateAsync(printingEdition);
            if (!result)
            {
                printingEditionModel.Errors.Add(Constants.Errors.DeletePringtingEditionError);
            }

            return printingEditionModel;
        }
    }
}
