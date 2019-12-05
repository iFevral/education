using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Common;
using Store.BusinessLogic.Models.Filters;

namespace Store.Presentation.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = Constants.RoleNames.Admin)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("~/[controller]s/")]
        [HttpPost]
        public async Task<IActionResult> GetAllUsers([FromBody]UserFilterModel userFilter)
        {
            var userModel = await _userService.GetAllUsersAsync(userFilter);
            if (userModel.Errors.Count > 0)
            {
                return NotFound(userModel);
            }

            return Ok(userModel);
        }

        [Route("~/[controller]s/Count")]
        [HttpPost]
        public async Task<IActionResult> GetNumber()
        {
            int counter = await _userService.GetNumberOfUsers();

            return Ok(counter);
        }

        [Route("~/[controller]s/[controller]/{username}")]
        [HttpPost]
        public async Task<IActionResult> GetUserProfile(string username)
        {
            var userModel = await _userService.GetUserByEmailAsync(username);
            if (userModel.Errors.Count > 0)
            {
                return NotFound(userModel);
            }
            return Ok(userModel);
        }

        [Route("~/[controller]s/Block")]
        [HttpPost]
        public async Task<IActionResult> BlockUser(string username, bool enabled)
        {
            var userModel = await _userService.BlockUserAsync(username,enabled);
            if (userModel.Errors.Count > 0)
            {
                return NotFound(userModel);
            }
            return Ok(userModel);
        }

        [Route("~/[controller]s/Update")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]SignUpModel signUpData)
        {
            var userModel = await _userService.UpdateUserAsync(signUpData);
            if (userModel.Errors.Count > 0)
            {
                return NotFound(userModel);
            }
            return Ok(userModel);
        }

        [Route("~/[controller]s/Delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string username)
        {
            var userModel = await _userService.DeleteUserAsync(username);
            if (userModel.Errors.Count > 0)
            {
                return NotFound(userModel);
            }
            return Ok(userModel);
        }
    }
}