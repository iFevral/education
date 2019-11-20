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
            var users = await _userService.GetAllUsers();
            if (users.Users.Count != 0)
                return Ok(users.Users);

            return NotFound();
        }

        [Route("~/[controller]/Profile")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetUserProfile(string username)
        {
            UserModel user = new UserModel();
            user.Users.Add(await _userService.GetUserByName(username));

            if (user != null)
                return Ok(user);

            user.Errors.Add("User not found");
            
            return NotFound(user);
        }

        [Route("~/[controller]/Blocking")]
        [HttpGet]
        public async Task<IActionResult> BlockUser(string username)
        {
            return NotFound();
        }
    }
}