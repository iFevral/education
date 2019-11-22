using AutoMapper;
using System;
using System.Linq;
using System.Collections.Generic;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.Repositories.EFRepository;
using Store.DataAccess.Entities;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {

        private IMapper _mapper;
        private IPrintingEditionRepository _printingEditionRepository;

        public PrintingEditionService(ApplicationContext db, IMapper mapper)
        {
            _mapper = mapper;
            _printingEditionRepository = new PrintingEditionRepository(db);
        }

        public PrintingEditionModel GetAll(PrintingEditionFilter peFilter,int startIndex = -1, int quantity = -1)
        {
            Func<PrintingEditions,bool> predicate = pe => (peFilter.Title == null || pe.Title.ToLower().Contains(peFilter.Title.ToLower())) &&
                                                                           (peFilter.MinPrice == null || pe.Price >= peFilter.MinPrice) &&
                                                                           (peFilter.MaxPrice == null || pe.Price <= peFilter.MaxPrice) &&
                                                                           (peFilter.Author == null || pe.AuthorInBooks.Where(aib => aib.Author.Name.ToLower().Contains(peFilter.Author.ToLower())).Any());
            var printingEditionModel = new PrintingEditionModel();

            IList<PrintingEditions> printingEditions;
            if (startIndex != -1 && quantity != -1)
                printingEditions = _printingEditionRepository.Get(predicate,startIndex,quantity);
            else
                printingEditions = _printingEditionRepository.GetAll(predicate);

            if (printingEditions.Count == 0)
            {
                printingEditionModel.Errors.Add($"Printing editions not found");
                return printingEditionModel;
            }

            foreach( var printingEdition in printingEditions)
            {
                var pe = _mapper.Map<PrintingEditionModelItem>(printingEdition);
                foreach (var author in printingEdition.AuthorInBooks)
                {
                    var a = _mapper.Map<AuthorModelItem>(author.Author);
                    pe.Authors.Add(a);
                }

                printingEditionModel.PrintingEditions.Add(pe);
            }

            return printingEditionModel;
        }

        public async Task<PrintingEditionModel> FindById(int id)
        {
            var printingEditionModel = new PrintingEditionModel();
            var printingEdition = await _printingEditionRepository.FindByIdAsync(id);
            if (printingEdition == null)
            {
                printingEditionModel.Errors.Add("Printing edition not found");
                return printingEditionModel;
            }

            var pe = _mapper.Map<PrintingEditionModelItem>(printingEdition);
            foreach (var author in printingEdition.AuthorInBooks)
            {
                var a = _mapper.Map<AuthorModelItem>(author.Author);
                pe.Authors.Add(a);
            }

            printingEditionModel.PrintingEditions.Add(pe);
           
            return printingEditionModel;
        }

        public async Task<PrintingEditionModel> Create(PrintingEditionModelItem printingEditionItem)
        {
            var printingEditionModel = new PrintingEditionModel();
            var printingEdition = _mapper.Map<PrintingEditions>(printingEditionItem);
            
            printingEdition.AuthorInBooks = new List<AuthorInBooks>();
            foreach (var authorItem in printingEditionItem.Authors)
                printingEdition.AuthorInBooks.Add(new AuthorInBooks { AuthorId = authorItem.Id });

            await _printingEditionRepository.CreateAsync(printingEdition);
            return printingEditionModel;
        }

        public async Task<PrintingEditionModel> Update(int id, PrintingEditionModelItem printingEditionItem)
        {
            var printingEditionModel = new PrintingEditionModel();
            var printingEdition = await _printingEditionRepository.FindByIdAsync(id);
            if(printingEdition == null)
            {
                printingEditionModel.Errors.Add("Printing edition not found");
                return printingEditionModel;
            }

            _mapper.Map<PrintingEditionModelItem, PrintingEditions>(printingEditionItem, printingEdition);
            _printingEditionRepository.RemoveAuthors(printingEdition.Id);

            foreach (var authorItem in printingEditionItem.Authors)
            {
                if (printingEdition.AuthorInBooks == null) 
                    printingEdition.AuthorInBooks = new List<AuthorInBooks>();

                printingEdition.AuthorInBooks.Add(new AuthorInBooks { AuthorId = authorItem.Id });
            }
            await _printingEditionRepository.UpdateAsync(printingEdition);
            return printingEditionModel;
        }

        public async Task<PrintingEditionModel> Delete(int id)
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
