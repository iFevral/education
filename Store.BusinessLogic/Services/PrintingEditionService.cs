using AutoMapper;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.EFRepository;
using Store.DataAccess.Repositories.Interfaces;
using System.Linq;

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

        public PrintingEditionModel GetAll(PrintingEditionFilter peFilter)
        {
            var printingEditionModel = new PrintingEditionModel();
            var printingEditions = _printingEditionRepository.GetAll(pe => (peFilter.Title == null || pe.Title.ToLower().Contains(peFilter.Title.ToLower())) &&
                                                                           (peFilter.MinPrice == null || pe.Price >= peFilter.MinPrice) &&
                                                                           (peFilter.MaxPrice == null || pe.Price <= peFilter.MaxPrice) &&
                                                                           (peFilter.Author == null || pe.AuthorInBooks.Where(aib => aib.Author.Name.ToLower().Contains(peFilter.Author.ToLower())).Any()));
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

                printingEditionModel.printingEditions.Add(pe);
            }

            return printingEditionModel;
        }
    }
}
