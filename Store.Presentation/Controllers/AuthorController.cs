using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Models.Authors;
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
        [HttpGet]
        public async Task<IActionResult> GetAuthors([FromBody]AuthorFilter authorFilter, string sortBy = "Id", int startIndex = 0, int quantity = 1)
        {
            var authorModel = await _authorService.GetAll(authorFilter, sortBy, startIndex, quantity);
            if (authorModel.Errors.Count > 0)
            {
                return NotFound(authorModel);
            }

            return Ok(authorModel);
        }

        [Route("~/[controller]s/[controller]/{id}")]
        [HttpGet]
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

        [Route("~/[controller]s/Update/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody]AuthorModelItem authorItem)
        {
            var authorModel = await _authorService.UpdateAsync(id, authorItem);
            if (authorModel.Errors.Count > 0)
            {
                return NotFound(authorModel);
            }

            return Ok(authorModel);
        }

        [Route("~/[controller]s/Delete/{id}")]
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