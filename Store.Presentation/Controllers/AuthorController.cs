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
        public async Task<IActionResult> GetAllAsync([FromBody]AuthorFilterModel authorFilter)
        {
            var authorModel = await _authorService.GetAll(authorFilter);

            return Ok(authorModel);
        }

        [Route("~/[controller]s/Count")]
        [HttpPost]
        public async Task<IActionResult> GetNumberAsync()
        {
            int counter = await _authorService.GetNumberOfAuthors();

            return Ok(counter);
        }

        [Route("~/[controller]s/[controller]/{id}")]
        [HttpPost]
        public async Task<IActionResult> GetAsync(int id)
        {
            var authorModel = await _authorService.FindByIdAsync(id);

            return Ok(authorModel);
        }

        [Route("~/[controller]s/Create")]
        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpPut]
        public async Task<IActionResult> CreateAsync([FromBody]AuthorModelItem authorItem)
        {
            var authorModel = await _authorService.CreateAsync(authorItem);

            return Ok(authorModel);
        }

        [Route("~/[controller]s/Update/")]
        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]AuthorModelItem authorItem)
        {
            var authorModel = await _authorService.UpdateAsync(authorItem);

            return Ok(authorModel);
        }

        [Route("~/[controller]s/Delete/{id}")]
        [Authorize(Roles = Constants.RoleNames.Admin)]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var authorModel = await _authorService.DeleteAsync(id);

            return Ok(authorModel);
        }
    }
}