using System.Threading.Tasks;
using System.Collections.Generic;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.Entities;
using Store.BusinessLogic.Common.Mappers.Interface;
using Store.BusinessLogic.Common;

namespace Store.BusinessLogic.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {

        private readonly IMapper<PrintingEditions, PrintingEditionModelItem> _mapper;
        private readonly IPrintingEditionRepository _printingEditionRepository;

        public PrintingEditionService(IMapper<PrintingEditions, PrintingEditionModelItem> mapper,
                                      IPrintingEditionRepository printingEditionRepository)
        {
            _mapper = mapper;
            _printingEditionRepository = printingEditionRepository;
        }

        public PrintingEditionModel GetAll(PrintingEditionFilter peFilter,int startIndex = 0, int quantity = 0)
        {
            var printingEditionModel = new PrintingEditionModel();

            IList<PrintingEditions> printingEditions;
            if ( quantity != 0)
            {
                printingEditions = _printingEditionRepository.Get(peFilter.Predicate, startIndex, quantity);
            }
            else
            {
                printingEditions = _printingEditionRepository.GetAll(peFilter.Predicate);
            }

            if (printingEditions.Count == 0)
            {
                printingEditionModel.Errors.Add(Constants.Errors.NotFoundPringtingEditionError);
                return printingEditionModel;
            }

            foreach( var printingEdition in printingEditions)
            {
                var pe = new PrintingEditionModelItem();
                pe = _mapper.Map(printingEdition, pe);
                printingEditionModel.PrintingEditions.Add(pe);
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
            var pe = new PrintingEditionModelItem();
            pe = _mapper.Map(printingEdition, pe);
           
            return printingEditionModel;
        }

        public async Task<PrintingEditionModelItem> CreateAsync(PrintingEditionModelItem printingEditionModel)
        {
            var printingEdition = new PrintingEditions();
            printingEdition = _mapper.Map(printingEditionModel, printingEdition);
           
            var result = await _printingEditionRepository.CreateAsync(printingEdition);
            if (!result)
            {
                printingEditionModel.Errors.Add(Constants.Errors.CreatePringtingEditionError);
                return printingEditionModel;
            }
            return printingEditionModel;
        }

        public async Task<PrintingEditionModelItem> UpdateAsync(int id, PrintingEditionModelItem printingEditionModel)
        {
            var printingEdition = await _printingEditionRepository.FindByIdAsync(id);
            if(printingEdition == null)
            {
                printingEditionModel.Errors.Add(Constants.Errors.NotFoundPringtingEditionError);
                return printingEditionModel;
            }

            printingEdition =_mapper.Map(printingEditionModel, printingEdition);
            var result = await _printingEditionRepository.UpdateAsync(printingEdition);
            if(!result)
            {
                printingEditionModel.Errors.Add(Constants.Errors.UpdatePringtingEditionError);
                return printingEditionModel;
            }

            return printingEditionModel;
        }

        public async Task<PrintingEditionModelItem> DeleteAsync(int id)
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
                return printingEditionModel;
            }

            return printingEditionModel;
        }
    }
}
