using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.Presentation.Common;
using Store.Presentation.Helpers;

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
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
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<UserModelItem>> GetUsers()
        {
            return _userService.GetAllUsers().Items;
        }

        [Route("~/api/[controller]/SignIn")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<object> SignIn([FromBody] SignInModelItem loginData)
        {
            TokenModel token = new TokenModel();
            if (ModelState.IsValid)
            {
                UserModelItem user = await _userService.SignIn(loginData);
                if (user != null)
                    token = JwtHelper.GenerateJwtToken(user, _configuration);
            }
            return token;
        }

        [Route("~/api/[controller]/SignUp")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IEnumerable<string>> SignUp([FromBody] SignUpModelItem userData)
        {
            await _userService.SignUp(userData);

            //TODO: Добавить валидацию данных, изменить сложность пароля, добавить сообщения и ошибки.

            return new List<string> { "Success" };
        }
    }
}