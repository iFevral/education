using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {

        private IMapper _mapper;
        private IPrintingEditionRepository _printingEditionRepository;

        public PrintingEditionService(IMapper mapper,
                                      IPrintingEditionRepository printingEditionRepository)
        {
            _mapper = mapper;
            _printingEditionRepository = printingEditionRepository;
        }

        public PrintingEditionModel GetAll(PrintingEditionFilter peFilter,int startIndex = -1, int quantity = -1)
        {
            var printingEditionModel = new PrintingEditionModel();

            IList<PrintingEditions> printingEditions;
            if (startIndex != -1 && quantity != -1)
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
                var pe = _mapper.Map<PrintingEditionModelItem>(printingEdition);
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

            var pe = _mapper.Map<PrintingEditionModelItem>(printingEdition);

            printingEditionModel.PrintingEditions.Add(pe);
           
            return printingEditionModel;
        }

        public async Task<PrintingEditionModel> CreateAsync(PrintingEditionModelItem printingEditionItem)
        {
            var printingEditionModel = new PrintingEditionModel();
            var printingEdition = _mapper.Map<PrintingEditions>(printingEditionItem);
            
            printingEdition.AuthorInBooks = new List<AuthorInBooks>();

            await _printingEditionRepository.CreateAsync(printingEdition);
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

            _mapper.Map<PrintingEditionModelItem, PrintingEditions>(printingEditionItem, printingEdition);
            await _printingEditionRepository.UpdateAsync(printingEdition);
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

            await _printingEditionRepository.RemoveAsync(printingEdition);

            return printingEditionModel;
        }
    }
}
