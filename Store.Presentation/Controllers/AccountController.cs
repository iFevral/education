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
using Store.BusinessLogic.Helpers;
using Microsoft.AspNetCore.Http;

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
            var users = await _userService.GetAllUsers();
            return users.Items;
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
                {
                    user.AccessToken = JwtHelper.GenerateJwtToken(user, _configuration["JwtKey"], _configuration["AccessTokenExpireMinutes"]);
                    user.RefreshToken = JwtHelper.GenerateRefreshToken();
                }

                return user;
            }
            return "Error";
        }

        [Route("~/api/[controller]/SignUp")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<object> SignUp([FromBody] SignUpModelItem userData)
        {
            string registrationToken = await _userService.SignUp(userData);

            string subject = "Account confirmation";
            string body = "Confirmation link: https://localhost:44312/api/Account/ConfirmEmail?" +
                          "username=" + userData.UserName + "&token=" + registrationToken;
            EmailHelper.Send(userData.Email,subject,body,_configuration);
            return "Confirm your email";
        }

        [Route("~/api/[controller]/ConfirmEmail")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<object> ConfirmEmail(string username, string token)
        {
            if (await _userService.ConfirmEmail(username, token))
                return "Email has successfully confirmed";
            else
                return "Verification error";
        }

        [Route("~/api/[controller]/ForgotPassword")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<object> ForgotPassword([FromBody] EmailModelItem user)
        {
            string passwordResetToken = await _userService.ResetPassword(user.Email);

            string subject = "Reseting password confirmation";
            string body = "Confirmation link: https://localhost:44312/api/Account/ConfirmResetPassword?" +
                          "email=" + user.Email + "&token=" + passwordResetToken;
            EmailHelper.Send(user.Email, subject, body, _configuration);
            return "Check your email";
        }

        [Route("~/api/[controller]/ConfirmResetPassword")]
        [AllowAnonymous]
        [HttpGet]
        public ForgotPasswordModelItem ConfirmResetPassword(string email, string token)
        {
            return new ForgotPasswordModelItem
            {
                Email = email,
                Token = token
            };
        }

        [Route("~/api/[controller]/ConfirmNewPassword")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<object> ConfirmNewPassword([FromBody] ForgotPasswordModelItem user)
        {
            if (await _userService.ConfirmNewPassword(user.Email, user.Token, user.Password))
                return "Password has successfully changed";
            else
                return "Verification error";
        }
    }
}