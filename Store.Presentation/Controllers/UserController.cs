using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.BusinessLogic.Models.Roles;

namespace Store.Presentation.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(UserManager<Users> um,
                              RoleManager<Roles> rm,
                              IMapper mapper)
        {
            _userService = new UserService(um, rm, mapper);
        }

        [Route("~/[controller]/GetAll")]
        [HttpPost]
        public async Task<IActionResult> GetAllUsers([FromBody]UserFilter userFilter)
        {
            var userModel = _userService.GetAllUsers(userFilter);
            if (userModel.Errors.Count > 0)
                return NotFound(userModel.Errors);

            return Ok(userModel.Users);
        }

        [Route("~/[controller]/Profile")]
        [HttpGet]
        public async Task<IActionResult> GetUserProfile(string username)
        {
            var userModel = await _userService.GetUserByName(username);
            if (userModel.Errors.Count > 0)
                return NotFound(userModel.Errors);

            return Ok(userModel.Users);
        }

        [Route("~/[controller]/Block")]
        [HttpPost]
        public async Task<IActionResult> BlockUser(string username, bool enabled)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userModel = await _userService.BlockUser(username,enabled);
            if (userModel.Errors.Count > 0)
                return NotFound(userModel.Errors);

            var user = userModel.Users[0];
            return Ok($"User '{user.Username}' has succesfuly blocked'");
        }

        [Route("~/[controller]/Create")]
        [HttpPut]
        public async Task<IActionResult> Create([FromBody]SignUpData signUpData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userModel = await _userService.CreateUser(signUpData);
            if (userModel.Errors.Count > 0)
                return NotFound(userModel.Errors);

            var user = userModel.Users[0];
            return Ok($"User '{user.Username}' has succesfuly created'");
        }

        [Route("~/[controller]/Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody]SignUpData signUpData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userModel = await _userService.EditUser(signUpData);
            if (userModel.Errors.Count > 0)
                return NotFound(userModel.Errors);

            var user = userModel.Users[0];
            return Ok($"User '{user.Username}' has succesfuly edited'");
        }

        [Route("~/[controller]/Delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string username)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userModel = await _userService.DeleteUser(username);
            if (userModel.Errors.Count > 0)
                return NotFound(userModel.Errors);

            var user = userModel.Users[0];
            return Ok($"User '{user.Username}' has succesfuly deleted'");
        }

        [Route("~/[controller]/CreateRole")]
        [HttpPut]
        public async Task<IActionResult> CreateRole([FromBody]RoleModelItem roleModelItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var roleModel = await _userService.CreateRole(roleModelItem);
            if (roleModel.Errors.Count > 0)
                return NotFound(roleModel.Errors);

            var user = roleModel.Roles[0];
            return Ok($"Role '{user.Role}' succesfuly created");
        }

        [Route("~/[controller]/DeleteRole")]
        [HttpDelete]
        public async Task<IActionResult> RemoveRole([FromBody]RoleModelItem roleModelItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var roleModel = await _userService.RemoveRole(roleModelItem);
            if (roleModel.Errors.Count > 0)
                return NotFound(roleModel.Errors);

            var user = roleModel.Roles[0];
            return Ok($"Role '{user.Role}' succesfuly deleted");
        }

        [Route("~/[controller]/AddToRole")]
        [HttpPut]
        public async Task<IActionResult> AddToRole([FromBody]UserRoleModelItem userRoleModelItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var roleModel = await _userService.AddUserToRole(userRoleModelItem);
            if (roleModel.Errors.Count > 0)
                return NotFound(roleModel.Errors);

            var user = roleModel.Users[0];
            return Ok($"User '{user.Username}' added to role '{user.Role}'");
        }

        [Route("~/[controller]/RemoveFromRole")]
        [HttpDelete]
        public async Task<IActionResult> RemoveFromRole([FromBody]UserRoleModelItem userRoleModelItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var roleModel = await _userService.RemoveUserFromRole(userRoleModelItem);
            if (roleModel.Errors.Count > 0)
                return NotFound(roleModel.Errors);

            var user = roleModel.Users[0];
            return Ok($"User '{user.Username}' removed from role '{user.Role}'");
        }
    }
}