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
        public async Task<IActionResult> GetAllAsync([FromBody]UserFilterModel userFilter)
        {
            var userModel = await _userService.GetAllUsersAsync(userFilter);

            return Ok(userModel);
        }

        [Route("~/[controller]s/[action]")]
        [HttpPost]
        public async Task<IActionResult> Count()
        {
            int counter = await _userService.GetNumberOfUsers();

            return Ok(counter);
        }

        [Route("~/[controller]s/[action]")]
        [HttpPost]
        public async Task<IActionResult> Profile([FromBody]UserModelItem userModel)
        {
            userModel = await _userService.GetUserByEmailAsync(userModel.Email);

            return Ok(userModel);
        }

        [Route("~/[controller]s/[action]")]
        [HttpPost]
        public async Task<IActionResult> Blocking([FromBody]UserModelItem userModel)
        {
            var model = await _userService.SetLockingStatus(userModel.Email, userModel.IsLocked);

            return Ok(model);
        }

        [Route("~/[controller]s/[action]")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]SignUpModel signUpData)
        {
            var userModel = await _userService.UpdateUserAsync(signUpData);

            return Ok(userModel);
        }

        [Route("~/[controller]s/[action]")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]UserModelItem userModel)
        {
            var model = await _userService.DeleteUserAsync(userModel.Email);

            return Ok(model);
        }
    }
}