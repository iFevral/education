using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Store.DataAccess.Entities;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services;
using Store.BusinessLogic.Services.Interfaces;
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
                              SignInManager<Users> sm,
                              IMapper mapper)
        {
            _userService = new UserService(um, rm, sm, mapper);
        }

        [Route("~/[controller]s/")]
        [HttpPost]
        public async Task<IActionResult> GetAllUsers([FromBody]UserFilter userFilter)
        {
            var userModel = await _userService.GetAllUsersAsync();
            if (userModel.Errors.Count > 0)
            {
                return NotFound(userModel);
            }

            return Ok(userModel);
        }

        [Route("~/[controller]s/[controller]/{username}")]
        [HttpGet]
        public async Task<IActionResult> GetUserProfile(string username)
        {
            var userModel = await _userService.GetUserByNameAsync(username);
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

        [Route("~/[controller]s/Create")]
        [HttpPut]
        public async Task<IActionResult> Create([FromBody]SignUpModel signUpData)
        {
            var userModel = await _userService.CreateUserAsync(signUpData);
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

        [Route("~/[controller]s/CreateRole")]
        [HttpPut]
        public async Task<IActionResult> CreateRole([FromBody]RoleModelItem roleModelItem)
        {
            var roleModel = await _userService.CreateRoleAsync(roleModelItem);
            if (roleModel.Errors.Count > 0)
            {
                return NotFound(roleModel);
            }
            return Ok(roleModel);
        }

        [Route("~/[controller]s/DeleteRole")]
        [HttpDelete]
        public async Task<IActionResult> RemoveRole([FromBody]RoleModelItem roleModelItem)
        {
            var roleModel = await _userService.RemoveRoleAsync(roleModelItem);
            if (roleModel.Errors.Count > 0)
            {
                return NotFound(roleModel);
            }
            return Ok(roleModel);
        }

        [Route("~/[controller]s/AddToRole")]
        [HttpPut]
        public async Task<IActionResult> AddToRole([FromBody]UserRoleModelItem userRoleModelItem)
        {
            var roleModel = await _userService.AddUserToRoleAsync(userRoleModelItem);
            if (roleModel.Errors.Count > 0)
            {
                return NotFound(roleModel);
            }
            return Ok(roleModel);
        }

        [Route("~/[controller]s/RemoveFromRole")]
        [HttpDelete]
        public async Task<IActionResult> RemoveFromRole([FromBody]UserRoleModelItem userRoleModelItem)
        {
            var roleModel = await _userService.RemoveUserFromRoleAsync(userRoleModelItem);
            if (roleModel.Errors.Count > 0)
            {
                return NotFound(roleModel);
            }
            return Ok(roleModel);
        }
    }
}