using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Store.BusinessLogic.Services;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.AppContext;
using Microsoft.Extensions.Configuration;
using Store.Presentation.Helpers;

namespace Store.Presentation.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _configuration;
        private IUserService _userService;

        public UserController(IConfiguration configuration,
                                 ApplicationContext db,
                                 UserManager<Users> um,
                                 RoleManager<Roles> rm,
                                 SignInManager<Users> sim,
                                 IMapper mapper)
        {
            _configuration = configuration;
            _userService = new UserService(db, um, rm, sim, mapper);
        }

        [Route("~/[controller]")]
        [HttpGet]
        public async Task<IEnumerable<UserModelItem>> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            return users.Items;
        }

        [Route("~/[controller]/Profile")]
        [Authorize]
        [HttpPost]
        public async Task<UserModelItem> GetUserProfile(string Autorization)
        {
            string token = Autorization.Substring(7); //Remove 'Bearer ' from token
            var user = await _userService.GetUserById(JwtHelper.GetUserIdFromToken(token));
            return user;
        }

        [Route("~/[controller]/Blocking")]
        [HttpGet]
        public async Task<UserModelItem> BlockUser(string username)
        {
            var user = await _userService.GetUserById(username);
            return user;
        }
    }
}