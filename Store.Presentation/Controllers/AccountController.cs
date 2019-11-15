using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Store.BusinessLogic.Services;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.AppContext;
using Store.Presentation.Helpers;

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration _configuration;
        private IUserService _userService;

        public AccountController(IConfiguration configuration, 
                                 ApplicationContext db,
                                 UserManager<Users> um,
                                 RoleManager<Roles> rm,
                                 SignInManager<Users> sim,
                                 IMapper mapper)
        {
            _configuration = configuration;
            _userService = new UserService(db, um, rm, sim, mapper);
        }

        [Route("~/api/[controller]")]
        [HttpGet]
        public async Task<IEnumerable<UserModelItem>> GetUsers()
        {
            return _userService.GetAllUsers().Items;
        }

        [Route("~/api/[controller]/GetUsers4Admin")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IEnumerable<UserModelItem>> GetUsers4Admin()
        {
            return _userService.GetAllUsers().Items;
        }

        [Route("~/api/[controller]/SignIn")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<object> SignIn([FromBody] SignInModelItem loginData)
        {
            if (ModelState.IsValid)
            {
                UserModelItem user = await _userService.SignIn(loginData);
                if (user != null)
                    user.AccessToken = JwtHelper.GenerateJwtToken(user, _configuration["JwtKey"], _configuration["AccessTokenExpireMinutes"]);
                return user;
            }
            return "Error";
        }

        [Route("~/api/[controller]/SignUp")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<object> SignUp([FromBody] SignUpModelItem userData)
        {
            UserModelItem user = await _userService.SignUp(userData);
            if (user != null)
                user.AccessToken = JwtHelper.GenerateJwtToken(user, _configuration["JwtKey"], _configuration["AccessTokenExpireMinutes"]);

            return user;
        }
        //TODO: Добавить валидацию данных, изменить сложность пароля, добавить сообщения и ошибки.
    }
}