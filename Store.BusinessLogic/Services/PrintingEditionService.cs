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
        private readonly IAuthorInPrintingEditionRepository _authorInPrintingEditionRepository;

        public PrintingEditionService(IPrintingEditionRepository printingEditionRepository,
                                      IAuthorInPrintingEditionRepository authorInPrintingEditionRepository)
        {
            _printingEditionRepository = printingEditionRepository;
            _authorInPrintingEditionRepository = authorInPrintingEditionRepository;
        }

        public async Task<PrintingEditionModel> GetAllAsync(PrintingEditionFilterModel printingEditionFilterModel)
        {
            var printingEditionModel = new PrintingEditionModel();

            IEnumerable<PrintingEdition> printingEditions;
            var filterModel = printingEditionFilterModel.MapToEFFilterModel();
            
            printingEditions = _printingEditionRepository.GetAll(filterModel, out int counter);
            

            if (printingEditions == null)
            {
                printingEditionModel.Errors.Add(Constants.Errors.NotFoundPringtingEditionError);
                return printingEditionModel;
            }

            printingEditionModel.Counter = counter;

            foreach( var printingEdition in printingEditions)
            {
                var item = printingEdition.MapToModel();
                printingEditionModel.Items.Add(item);
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

        public async Task<BaseModel> UpdateAsync(PrintingEditionModelItem printingEditionModel)
        {
            var printingEdition = await _printingEditionRepository.FindByIdAsync(printingEditionModel.Id);
            if(printingEdition == null)
            {
                printingEditionModel.Errors.Add(Constants.Errors.NotFoundPringtingEditionError);
                return printingEditionModel;
            }

            printingEdition = printingEditionModel.MapToEntity(printingEdition);
            
            var result = await _authorInPrintingEditionRepository.RemoveAuthorsInPrintingEditions(printingEdition.Id);
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
