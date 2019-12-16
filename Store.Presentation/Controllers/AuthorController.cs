using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Common;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Models.Filters;
using Store.BusinessLogic.Services.Interfaces;

namespace Store.Presentation.Controllers
{
    [Route("[controller]s")]
    [Authorize(Roles = Constants.RoleNames.Admin)]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpPost]
        public async Task<IActionResult> GetAll([FromHeader]string Authorization, [FromBody]AuthorFilterModel authorFilter)
        {
            var authorModel = await _authorService.GetAllAsync(authorFilter);

            return Ok(authorModel);
        }

        [Route("~/[controller]s/{id}")]
        [HttpPost]
        public async Task<IActionResult> Get(int id)
        {
            var authorModel = await _authorService.FindByIdAsync(id);

            return Ok(authorModel);
        }

        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpPut]
        public async Task<IActionResult> Create([FromBody]AuthorModelItem authorItem)
        {
            var authorModel = await _authorService.CreateAsync(authorItem);

            return Ok(authorModel);
        }

        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpPatch]
        public async Task<IActionResult> Update([FromBody]AuthorModelItem authorItem)
        {
            var authorModel = await _authorService.UpdateAsync(authorItem);

            return Ok(authorModel);
        }

        [Route("~/[controller]s/{id}")]
        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var authorModel = await _authorService.DeleteAsync(id);

            return Ok(authorModel);
        }
    }
}