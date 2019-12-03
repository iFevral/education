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

        public async Task<PrintingEditionModel> GetAll(PrintingEditionFilter peFilter)
        {
            var printingEditionModel = new PrintingEditionModel();

            IEnumerable<PrintingEditions> printingEditions;
            if (peFilter.Quantity != 0)
            {
                printingEditions = await _printingEditionRepository.GetAsync(peFilter.Predicate,
                                                                             peFilter.StartIndex,
                                                                             peFilter.Quantity,
                                                                             peFilter.SortProperty, 
                                                                             peFilter.SortWay);
            }
            else
            {
                printingEditions = await _printingEditionRepository.GetAllAsync(peFilter.Predicate,
                                                                                peFilter.SortProperty,
                                                                                peFilter.SortWay);
            }

            if (printingEditions == null)
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
