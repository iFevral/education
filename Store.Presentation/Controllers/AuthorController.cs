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
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [Route("~/[controller]s")]
        [HttpPost]
        public async Task<IActionResult> GetAuthors([FromBody]AuthorFilterModel authorFilter)
        {
            var authorModel = await _authorService.GetAll(authorFilter);
            if (authorModel.Errors.Count > 0)
            {
                return NotFound(authorModel);
            }

            return Ok(authorModel);
        }

        [Route("~/[controller]s/[controller]/{id}")]
        [HttpPost]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var authorModel = await _authorService.FindByIdAsync(id);
            if (authorModel.Errors.Count > 0)
            {
                return NotFound(authorModel);
            }

            return Ok(authorModel);
        }

        [Route("~/[controller]s/Create")]
        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpPut]
        public async Task<IActionResult> CreateAuthor([FromBody]AuthorModelItem authorItem)
        {
            var authorModel = await _authorService.CreateAsync(authorItem);
            if (authorModel.Errors.Count > 0)
            {
                return NotFound(authorModel);
            }

            return Ok(authorModel);
        }

        [Route("~/[controller]s/Update/")]
        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpPut]
        public async Task<IActionResult> UpdateAuthor([FromBody]AuthorModelItem authorItem)
        {
            var authorModel = await _authorService.UpdateAsync(authorItem);
            if (authorModel.Errors.Count > 0)
            {
                return NotFound(authorModel);
            }

            return Ok(authorModel);
        }

        [Route("~/[controller]s/Delete/{id}")]
        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpDelete]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var authorModel = await _authorService.DeleteAsync(id);
            if (authorModel.Errors.Count > 0)
            {
                return NotFound(authorModel);
            }

            return Ok(authorModel);
        }
    }
}