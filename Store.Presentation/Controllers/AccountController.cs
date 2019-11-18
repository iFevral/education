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

namespace Store.Presentation.Controllers
{
    [Route("[controller]")]
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

        [Route("~/[controller]")]
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<UserModelItem>> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            return users.Items;
        }

        [Route("~/[controller]/SignIn")]
        [HttpPost]
        public async Task<object> SignIn([FromBody] SignInModelItem loginData)
        {
            if (ModelState.IsValid)
            {
                UserModelItem user = await _userService.SignIn(loginData);
                if (user != null)
                {
                    user.AccessToken = JwtHelper.GenerateJwtAccessToken(user, _configuration);
                    user.RefreshToken = JwtHelper.GenerateJwtRefreshToken(user, _configuration);
                }

                return user;
            }
            return "Error";
        }

        [Route("~/[controller]/SignUp")]
        [HttpPost]
        public async Task<object> SignUp([FromBody] SignUpModelItem userData)
        {
            string registrationToken = await _userService.SignUp(userData);

            string subject = "Account confirmation";
            string body = "Confirmation link: <a href='https://localhost:44312/Account/ConfirmEmail?" +
                          "username=" + userData.UserName + "&token=" + registrationToken + "'>Verify email</a>";
            EmailHelper.Send(userData.Email,subject,body,_configuration);
            return "Check your email to confirm your information";
        }

        [Route("~/[controller]/ConfirmEmail")]
        [HttpGet]
        public async Task<object> ConfirmEmail(string username, string token)
        {
            token = token.Replace(" ", "+");
            if (await _userService.ConfirmEmail(username, token))
                return "Email has successfully confirmed";
            else
                return "Verification error";
        }

        [Route("~/[controller]/ForgotPassword")]
        [HttpPost]
        public async Task<object> ForgotPassword([FromBody] EmailModelItem user)
        {
            string passwordResetToken = await _userService.ResetPassword(user.Email);

            string subject = "Reseting password confirmation";
            string body = "Confirmation link: <a href='https://localhost:44312/Account/ConfirmResetPassword?" +
                          "email=" + user.Email + "&token=" + passwordResetToken + "'>Reset password</a>";
            EmailHelper.Send(user.Email, subject, body, _configuration);
            return "Check your email";
        }

        [Route("~/[controller]/ConfirmResetPassword")]
        [HttpGet]
        public ForgotPasswordModelItem ConfirmResetPassword(string email, string token)
        {
            token = token.Replace(" ", "+");
            return new ForgotPasswordModelItem
            {
                Email = email,
                Token = token.Replace(" ", "+")
            };
        }

        [Route("~/[controller]/ConfirmNewPassword")]
        [HttpPost]
        public async Task<object> ConfirmNewPassword([FromBody] ForgotPasswordModelItem user)
        {
            user.Token = user.Token.Replace(" ", "+");
            await _userService.ConfirmNewPassword(user.Email, user.Token, user.Password);
            return "Password has successfully changed";
        }

        [Route("~/RefreshToken")]
        [Authorize]
        [HttpPost]
        public async Task<TokenModelItem> RefreshTokens([FromHeader]string username)
        {
            var user = await _userService.GetUser(username);
            return new TokenModelItem {
                AccessToken = JwtHelper.GenerateJwtAccessToken(user, _configuration),
                RefreshToken = JwtHelper.GenerateJwtRefreshToken(user, _configuration)
            };
        }
    }
}