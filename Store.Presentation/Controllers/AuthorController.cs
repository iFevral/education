using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.AppContext;

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private IAuthorService _authorService;

        public AuthorController(ApplicationContext db,
                                IMapper mapper)
        {
            _authorService = new AuthorService(db, mapper);
        }

        [Route("~/[controller]/GetAll")]
        [HttpGet]
        public IActionResult GetAuthors(string name, int startIndex = -1, int quantity = -1)
        {
            var authorModel = _authorService.GetAll(name, startIndex, quantity);
            if (authorModel.Errors.Count > 0)
                return NotFound(authorModel.Errors);

            return Ok(authorModel.Authors);
        }

        [Route("~/[controller]/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var authorModel = await _authorService.FindById(id);
            if (authorModel.Errors.Count > 0)
                return NotFound(authorModel.Errors);

            return Ok(authorModel.Authors);
        }

        [Route("~/[controller]/Create")]
        [HttpPut]
        public async Task<IActionResult> CreateAuthor([FromBody]AuthorModelItem authorItem)
        {
            var authorModel = await _authorService.Create(authorItem);
            if (authorModel.Errors.Count > 0)
                return NotFound(authorModel.Errors);

            return Ok(authorModel.Authors);
        }

        [Route("~/[controller]/Update/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody]AuthorModelItem authorItem)
        {
            var authorModel = await _authorService.Update(id, authorItem);
            if (authorModel.Errors.Count > 0)
                return NotFound(authorModel.Errors);

            return Ok(authorModel.Authors);
        }

        [Route("~/[controller]/Delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var authorModel = await _authorService.Delete(id);
            if (authorModel.Errors.Count > 0)
                return NotFound(authorModel.Errors);

            return Ok(authorModel.Authors);
        }
    }
}