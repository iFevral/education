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

        [Route("~/[controller]s/Count")]
        [HttpPost]
        public async Task<IActionResult> GetNumberAsync()
        {
            int counter = await _userService.GetNumberOfUsers();

            return Ok(counter);
        }

        [Route("~/[controller]s/[controller]")]
        [HttpPost]
        public async Task<IActionResult> GetProfileAsync([FromBody]UserModelItem userModel)
        {
            userModel = await _userService.GetUserByEmailAsync(userModel.Email);
 
            return Ok(userModel);
        }

        [Route("~/[controller]s/Block")]
        [HttpPost]
        public async Task<IActionResult> BlockAsync([FromBody]UserModelItem userModel)
        {
            var model = await _userService.BlockUserAsync(userModel.Email, userModel.IsLocked);

            return Ok(model);
        }

        [Route("~/[controller]s/Update")]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]SignUpModel signUpData)
        {
            var userModel = await _userService.UpdateUserAsync(signUpData);

            return Ok(userModel);
        }

        [Route("~/[controller]s/Delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody]UserModelItem userModel)
        {
            var model = await _userService.DeleteUserAsync(userModel.Email);
 
            return Ok(model);
        }
    }
}