using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Common;
using Store.BusinessLogic.Models.Filters;

namespace Store.Presentation.Controllers
{
    [Route("[controller]s")]
    [Authorize(Roles = Constants.RoleNames.Admin)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> GetAllAsync([FromBody]UserFilterModel userFilter)
        {
            var userModel = await _userService.GetAllUsersAsync(userFilter);

            return Ok(userModel);
        }

        [Route("~/[controller]s/{id}")]
        [HttpPost]
        public async Task<IActionResult> Profile(long id)
        {
            var userModel = await _userService.GetUserAsync(id);

            return Ok(userModel);
        }

        [Route("~/[controller]s/[action]")]
        [HttpPost]
        public async Task<IActionResult> Block([FromBody]UserModelItem userModel)
        {
            var model = await _userService.SetLockingStatus(userModel.Email, userModel.IsLocked);

            return Ok(model);
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody]SignUpModel signUpData)
        {
            var userModel = await _userService.UpdateUserAsync(signUpData);

            return Ok(userModel);
        }

        [Route("~/[controller]s/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var model = await _userService.DeleteUserAsync(id);

            return Ok(model);
        }
    }
}