using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Common;
using Store.BusinessLogic.Models.Filters;

namespace Store.Presentation.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class PrintingEditionController : ControllerBase
    {
        private IPrintingEditionService _printingEditionService;

        public PrintingEditionController(IPrintingEditionService printingEditionService)
        {
            _printingEditionService = printingEditionService;
        }

        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody]PrintingEditionFilterModel printingEditionFilter)
        {
            var printingEditionModel = await _printingEditionService.GetAllAsync(printingEditionFilter);

            return Ok(printingEditionModel);
        }

        [Route("~/[controller]s/{id}")]
        [HttpPost]
        public async Task<IActionResult> Get(int id)
        {
            var printingEditionModel = await _printingEditionService.FindByIdAsync(id);

            return Ok(printingEditionModel);
        }

        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpPut]
        public async Task<IActionResult> Create([FromBody]PrintingEditionModelItem printingEditionItem)
        {
            var printingEditionModel = await _printingEditionService.CreateAsync(printingEditionItem);

            return Ok(printingEditionModel);
        }

        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpPatch]
        public async Task<IActionResult> Update([FromBody]PrintingEditionModelItem printingEditionItem)
        {
            var printingEditionModel = await _printingEditionService.UpdateAsync(printingEditionItem);

            return Ok(printingEditionModel);
        }

        [Route("~/[controller]s/{id}")]
        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var printingEditionModel = await _printingEditionService.DeleteAsync(id);

            return Ok(printingEditionModel);
        }
    }
}
