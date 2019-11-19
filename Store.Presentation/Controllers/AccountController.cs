using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Store.DataAccess.Entities;
using Store.Presentation.Helpers;
using Store.BusinessLogic.Helpers;
using Store.DataAccess.AppContext;
using Store.BusinessLogic.Services;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;

namespace Store.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration _configuration;
        private IAccountService _accountService;

        public AccountController(IConfiguration configuration,
                                 ApplicationContext db,
                                 UserManager<Users> um,
                                 RoleManager<Roles> rm,
                                 SignInManager<Users> sim,
                                 IMapper mapper)
        {
            _configuration = configuration;
            _accountService = new AccountService(db, um, rm, sim, mapper);
        }

        [Route("~/[controller]/Home")]
        [HttpGet]
        public IActionResult Home()
        {
            return Ok("Home Page");
        }

        [Route("~/[controller]/SignIn")]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInModelItem loginData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserModelItem user = await _accountService.SignIn(loginData);
            if (user != null)
            {
                user.AccessToken = JwtHelper.GenerateJwtAccessToken(user, _configuration);
                user.RefreshToken = JwtHelper.GenerateJwtRefreshToken(user, _configuration);
                return Ok(user);
            }

            return BadRequest("Invalid username of password");
        }

        [Route("~/[controller]/SignUp")]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpModelItem userData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string registrationToken = await _accountService.SignUp(userData);

            string subject = "Account confirmation";
            string body = "Confirmation link: <a href='https://localhost:44312/Account/ConfirmEmail?" +
                            "username=" + userData.UserName + "&token=" + registrationToken + "'>Verify email</a>";
            EmailHelper.Send(userData.Email, subject, body, _configuration);
            return Ok("Check your email to confirm your information");
        }

        [Route("~/[controller]/ConfirmEmail")]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string username, string token)
        {
            if (await _accountService.ConfirmEmail(username, token))
                 Ok("Email has successfully confirmed");
           
            
            return BadRequest("Verification error");
        }

        [Route("~/[controller]/ForgotPassword")]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromBody] EmailModelItem user)
        {
            string passwordResetToken = await _accountService.ResetPassword(user.Email);

            string subject = "Reseting password confirmation";
            string body = "Confirmation link: <a href='https://localhost:44312/Account/ConfirmResetPassword?" +
                          "email=" + user.Email + "&token=" + passwordResetToken + "'>Reset password</a>";
            EmailHelper.Send(user.Email, subject, body, _configuration);
            return Ok("Check your email");
        }

        [Route("~/[controller]/ConfirmReseting")]
        [HttpGet]
        public async Task<IActionResult> ConfirmResetPassword(string email, string token)
        {
            return Ok(new ForgotPasswordModelItem
            {
                Email = email,
                Token = token
            });
        }

        [Route("~/[controller]/ConfirmNewPassword")]
        [HttpPost]
        public async Task<IActionResult> ConfirmNewPassword([FromBody] ForgotPasswordModelItem user)
        {
            await _accountService.ConfirmNewPassword(user.Email, user.Token, user.Password);
            return Ok("Password has successfully changed");
        }

        [Route("~/RefreshToken")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RefreshTokens([FromHeader]string Authorization)
        {
            string token = Authorization.Substring(7); //Remove 'Bearer ' from token

            var user = await _accountService.GetUserById(JwtHelper.GetUserIdFromToken(token));
            return Ok(new TokenModelItem
            {
                AccessToken = JwtHelper.GenerateJwtAccessToken(user, _configuration),
                RefreshToken = JwtHelper.GenerateJwtRefreshToken(user, _configuration)
            });
        }
    }
}