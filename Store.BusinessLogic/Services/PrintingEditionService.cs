using System.Threading.Tasks;
using System.Collections.Generic;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.Entities;
using Store.BusinessLogic.Common.Mappers.Interface;

namespace Store.BusinessLogic.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {

        private IMapper<PrintingEditions, PrintingEditionModelItem> _mapper;
        private IPrintingEditionRepository _printingEditionRepository;

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
                printingEditionModel.Errors.Add($"Printing editions not found");
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

        public async Task<PrintingEditionModel> FindByIdAsync(int id)
        {
            var printingEditionModel = new PrintingEditionModel();
            var printingEdition = await _printingEditionRepository.FindByIdAsync(id);
            if (printingEdition == null)
            {
                printingEditionModel.Errors.Add("Printing edition not found");
                return printingEditionModel;
            }
            var pe = new PrintingEditionModelItem();
            pe = _mapper.Map(printingEdition, pe);

            printingEditionModel.PrintingEditions.Add(pe);
           
            return printingEditionModel;
        }

        public async Task<PrintingEditionModel> CreateAsync(PrintingEditionModelItem printingEditionItem)
        {
            var printingEditionModel = new PrintingEditionModel();
            var printingEdition = new PrintingEditions();
            printingEdition = _mapper.Map(printingEditionItem, printingEdition);
            
            printingEdition.AuthorInBooks = new List<AuthorInBooks>();

            var result = await _printingEditionRepository.CreateAsync(printingEdition);
            if (!result)
            {
                printingEditionModel.Errors.Add("Creating printing edition error");
                return printingEditionModel;
            }
            return printingEditionModel;
        }

        public async Task<PrintingEditionModel> UpdateAsync(int id, PrintingEditionModelItem printingEditionItem)
        {
            var printingEditionModel = new PrintingEditionModel();
            var printingEdition = await _printingEditionRepository.FindByIdAsync(id);
            if(printingEdition == null)
            {
                printingEditionModel.Errors.Add("Printing edition not found");
                return printingEditionModel;
            }

            printingEdition =_mapper.Map(printingEditionItem, printingEdition);
            var result = await _printingEditionRepository.UpdateAsync(printingEdition);
            if(!result)
            {
                printingEditionModel.Errors.Add("Updating printing edition error");
                return printingEditionModel;
            }

            return printingEditionModel;
        }

        public async Task<PrintingEditionModel> DeleteAsync(int id)
        {
            var printingEditionModel = new PrintingEditionModel();
            var printingEdition = await _printingEditionRepository.FindByIdAsync(id);
            if (printingEdition == null)
            {
                printingEditionModel.Errors.Add("Printing edition not found");
                return printingEditionModel;
            }

            printingEdition.isRemoved = true;
            var result = await _printingEditionRepository.UpdateAsync(printingEdition);
            if (!result)
            {
                printingEditionModel.Errors.Add("Removing printing edition error");
                return printingEditionModel;
            }

            return printingEditionModel;
        }
    }
}
