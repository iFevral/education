using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Services;
using Store.DataAccess.AppContext;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Models.PrintingEditions;

namespace Store.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PrintingEditionController : ControllerBase
    {
        private IPrintingEditionService _printingEditionService;

        public PrintingEditionController(ApplicationContext db,
                                         IMapper mapper)
        {
            _printingEditionService = new PrintingEditionService(db, mapper);
        }

        [Route("~/[controller]/GetAll")]
        [HttpGet]
        public async Task<ActionResult> GetPrintingEditions(string title, int? minPrice, int? maxPrice, string author)
        {
            PrintingEditionFilter peFilter = new PrintingEditionFilter()
            {
                Title = title,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Author = author
            };

            var printingEditionModel = _printingEditionService.GetAll(peFilter);
            if (printingEditionModel.Errors.Count > 0)
                return NotFound(printingEditionModel.Errors);

            return Ok(printingEditionModel.printingEditions);
        }
    }
}
