using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Models.PrintingEditions;
using Microsoft.AspNetCore.Authorization;

namespace Store.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PrintingEditionController : ControllerBase
    {
        private IPrintingEditionService _printingEditionService;

        public PrintingEditionController(IPrintingEditionService printingEditionService)
        {
            _printingEditionService = printingEditionService;
        }

        [Route("~/[controller]s")]
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
            {
                return NotFound(printingEditionModel);
            }

            return Ok(printingEditionModel);
        }

        [Route("~/[controller]s/[controller]/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetPrintingEdition(int id)
        {
            var printingEditionModel = await _printingEditionService.FindByIdAsync(id);
            if (printingEditionModel.Errors.Count > 0)
            {
                return NotFound(printingEditionModel.Errors);
            }

            return Ok(printingEditionModel.PrintingEditions);
        }

        [Route("~/[controller]s/Create")]
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> CreatePrintingEdition([FromBody]PrintingEditionModelItem printingEditionItem)
        {
            var printingEditionModel = await _printingEditionService.CreateAsync(printingEditionItem);
            if (printingEditionModel.Errors.Count > 0)
            {
                return NotFound(printingEditionModel.Errors);
            }

            return Ok(printingEditionModel.PrintingEditions);
        }

        [Route("~/[controller]s/Update/{id}")]
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdatePrintingEdition(int id, [FromBody]PrintingEditionModelItem printingEditionItem)
        {
            var printingEditionModel = await _printingEditionService.UpdateAsync(id, printingEditionItem);
            if (printingEditionModel.Errors.Count > 0)
            {
                return NotFound(printingEditionModel.Errors);
            }

            return Ok(printingEditionModel.PrintingEditions);
        }

        [Route("~/[controller]s/Delete/{id}")]
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeletePrintingEdition(int id)
        {
            var printingEditionModel = await _printingEditionService.DeleteAsync(id);
            if (printingEditionModel.Errors.Count > 0)
            {
                return NotFound(printingEditionModel.Errors);
            }
            return Ok(printingEditionModel.PrintingEditions);
        }
    }
}
