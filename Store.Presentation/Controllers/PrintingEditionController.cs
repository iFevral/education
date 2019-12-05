﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Models.PrintingEditions;
using Store.BusinessLogic.Common;
using Store.BusinessLogic.Models.Filters;

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
        [HttpPost]
        public async Task<IActionResult> GetPrintingEditions([FromBody]PrintingEditionFilterModel printingEditionFilter)
        {
            var printingEditionModel = await _printingEditionService.GetAll(printingEditionFilter);
            if (printingEditionModel.Errors.Count > 0)
            {
                return NotFound(printingEditionModel);
            }

            return Ok(printingEditionModel);
        }

        [Route("~/[controller]s/Count")]
        [HttpPost]
        public async Task<IActionResult> GetNumber()
        {
            int counter = await _printingEditionService.GetNumberOfPrintingEditions();

            return Ok(counter);
        }

        [Route("~/[controller]s/[controller]/{id}")]
        [HttpPost]
        public async Task<IActionResult> GetPrintingEdition(int id)
        {
            var printingEditionModel = await _printingEditionService.FindByIdAsync(id);
            if (printingEditionModel.Errors.Count > 0)
            {
                return NotFound(printingEditionModel);
            }

            return Ok(printingEditionModel);
        }

        [Route("~/[controller]s/Create")]
        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpPut]
        public async Task<IActionResult> CreatePrintingEdition([FromBody]PrintingEditionModelItem printingEditionItem)
        {
            var printingEditionModel = await _printingEditionService.CreateAsync(printingEditionItem);
            if (printingEditionModel.Errors.Count > 0)
            {
                return NotFound(printingEditionModel);
            }

            return Ok(printingEditionModel);
        }

        [Route("~/[controller]s/Update")]
        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpPut]
        public async Task<IActionResult> UpdatePrintingEdition([FromBody]PrintingEditionModelItem printingEditionItem)
        {
            var printingEditionModel = await _printingEditionService.UpdateAsync(printingEditionItem);
            if (printingEditionModel.Errors.Count > 0)
            {
                return NotFound(printingEditionModel);
            }

            return Ok(printingEditionModel);
        }

        [Route("~/[controller]s/Delete/{id}")]
        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpDelete]
        public async Task<IActionResult> DeletePrintingEdition(int id)
        {
            var printingEditionModel = await _printingEditionService.DeleteAsync(id);
            if (printingEditionModel.Errors.Count > 0)
            {
                return NotFound(printingEditionModel);
            }
            return Ok(printingEditionModel);
        }
    }
}
