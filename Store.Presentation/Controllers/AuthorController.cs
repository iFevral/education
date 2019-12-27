using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody]AuthorFilterModel authorFilter)
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

        [HttpPut]
        public async Task<IActionResult> Create([FromBody]AuthorModelItem authorItem)
        {
            var authorModel = await _authorService.CreateAsync(authorItem);

            return Ok(authorModel);
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody]AuthorModelItem authorItem)
        {
            var authorModel = await _authorService.UpdateAsync(authorItem);

            return Ok(authorModel);
        }

        [Route("~/[controller]s/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var authorModel = await _authorService.DeleteAsync(id);

            return Ok(authorModel);
        }
    }
}