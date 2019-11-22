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
        public IActionResult GetPrintingEditions(string title, int? minPrice, int? maxPrice, string author, int startIndex = -1, int quantity = -1)
        {
            PrintingEditionFilter peFilter = new PrintingEditionFilter()
            {
                Title = title,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Author = author
            };

            var printingEditionModel = _printingEditionService.GetAll(peFilter, startIndex, quantity);
            if (printingEditionModel.Errors.Count > 0)
                return NotFound(printingEditionModel.Errors);

            return Ok(printingEditionModel.PrintingEditions);
        }

        [Route("~/[controller]/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetPrintingEdition(int id)
        {
            var printingEditionModel = await _printingEditionService.FindById(id);
            if (printingEditionModel.Errors.Count > 0)
                return NotFound(printingEditionModel.Errors);

            return Ok(printingEditionModel.PrintingEditions);
        }

        [Route("~/[controller]/Create")]
        [HttpPut]
        public async Task<IActionResult> CreatePrintingEdition([FromBody]PrintingEditionModelItem printingEditionItem)
        {
            var printingEditionModel = await _printingEditionService.Create(printingEditionItem);
            if (printingEditionModel.Errors.Count > 0)
                return NotFound(printingEditionModel.Errors);

            return Ok(printingEditionModel.PrintingEditions);
        }

        [Route("~/[controller]/Update/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdatePrintingEdition(int id, [FromBody]PrintingEditionModelItem printingEditionItem)
        {
            var printingEditionModel = await _printingEditionService.Update(id, printingEditionItem);
            if (printingEditionModel.Errors.Count > 0)
                return NotFound(printingEditionModel.Errors);

            return Ok(printingEditionModel.PrintingEditions);
        }

        [Route("~/[controller]/Delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeletePrintingEdition(int id)
        {
            var printingEditionModel = await _printingEditionService.Delete(id);
            if (printingEditionModel.Errors.Count > 0)
                return NotFound(printingEditionModel.Errors);

            return Ok(printingEditionModel.PrintingEditions);
        }
    }
}
