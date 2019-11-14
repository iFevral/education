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
        [HttpGet]
        public async Task<IEnumerable<UserModelItem>> GetUsers()
        {
            return _userService.GetAllUsers().Items;
        }

        [Route("~/api/[controller]/SignIn")]
        [HttpPost]
        public async Task<object> SignIn([FromBody] SignInModelItem loginData)
        {
            if (ModelState.IsValid)
            {
                return JwtHelper.GenerateJwtToken(await _userService.SignIn(loginData),
                                                  _configuration);
            }
            else throw new ApplicationException("UNKNOWN_ERROR");
        }

        [Route("~/api/[controller]/SignUp")]
        [HttpPost]
        public async Task<IEnumerable<string>> SignUp([FromBody] SignUpModelItem userData)
        {
            await _userService.SignUp(userData);
            return new List<string> { "Success" };
        }
    }
}