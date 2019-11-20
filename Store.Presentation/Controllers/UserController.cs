using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Store.BusinessLogic.Services;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.AppContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using Store.BusinessLogic.Models.Users;

namespace Store.Presentation.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _configuration;
        private IUserService _userService;
        private ILogger _logger;

        public UserController(IConfiguration configuration,
                                 ApplicationContext db,
                                 UserManager<Users> um,
                                 RoleManager<Roles> rm,
                                 SignInManager<Users> sim,
                                 IMapper mapper,
                                 ILogger<UserController> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _userService = new UserService(db, um, rm, sim, mapper);
        }

        [Route("~/[controller]/GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {

            var userModel = await _userService.GetAllUsers();
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
            await _userService.BlockUser(username,enabled);
            return Ok();
        }

        [Route("~/[controller]/Create")]
        [HttpPut]
        public async Task<IActionResult> Create([FromBody]SignUpData signUpData)
        {
            await _userService.CreateUser(signUpData);
            return Ok();
        }

        [Route("~/[controller]/Edit")]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody]SignUpData signUpData)
        {
            await _userService.EditUser(signUpData);
            return Ok();
        }

        [Route("~/[controller]/Delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string username)
        {
            await _userService.DeleteUser(username);
            return Ok();
        }
    }
}