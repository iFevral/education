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

        [Route("~/[controller]/Profile")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetProfile([FromHeader]string Autorization)
        {
            //Remove "Bearer " from token
            string token = Autorization.Substring(7);

            var userModel = await _accountService.GetUserById(JwtHelper.GetUserIdFromToken(token));
            
            if (userModel.Errors.Count > 0)
                return NotFound(userModel.Errors);

            var user = userModel.Users[0];

            return Ok(user);

        }

        [Route("~/[controller]/SignIn")]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromHeader]string ipfingerprint, [FromBody] SignInData loginData)
        {
            if (!ModelState.IsValid)
                 return BadRequest(ModelState);

            var userModel = await _accountService.SignIn(loginData);
           
            if (userModel.Errors.Count > 0)
                return NotFound(userModel.Errors);

            var user = userModel.Users[0];

            if (await _accountService.IsAccountLocked(user.Username))
                return Unauthorized("User is blocked");

            var refreshToken = JwtHelper.GenerateJwtRefreshToken(userModel.Users[0], _configuration);
            await _accountService.SaveRefreshToken(user.Username, ipfingerprint, refreshToken);

            user.AccessToken = JwtHelper.GenerateJwtAccessToken(userModel.Users[0], _configuration);
            user.RefreshToken = refreshToken;

            return Ok(user);
        }

        [Route("~/[controller]/SignUp")]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpData userData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userModel = await _accountService.SignUp(userData);
            
            if (userModel.Errors.Count > 0)
                return BadRequest(userModel.Errors);

            var emailData = userModel.EmailData;
            string subject = "Account confirmation";
            string body = "Confirmation link: <a href='https://localhost:44312/Account/ConfirmEmail?" +
                            "username=" + emailData.Email + "&token=" + emailData.Token + "'>Verify email</a>";
            
            EmailHelper.Send(userData.Email, subject, body, _configuration);
            
            return Ok("Check your email to confirm your information");
        }

        [Route("~/[controller]/ConfirmEmail")]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string username, string token)
        {
            token = token.Replace(" ", "+");
            if (await _accountService.ConfirmEmail(username, token))
                 return Ok("Email has successfully confirmed");
           
            
            return BadRequest("Link not available");
        }

        [Route("~/[controller]/ForgotPassword")]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromBody] EmailData user)
        {
            var userModel = await _accountService.ResetPassword(user.Email);
            var resetPasswordData = userModel.ResetPasswordData;
            string subject = "Reseting password confirmation";
            string body = "Confirmation link: <a href='" + _configuration["Url"] + "/Account/ConfirmReseting?" +
                          "email=" + resetPasswordData.Email + "&token=" + resetPasswordData.Token + "'>Reset password</a>";
            
            EmailHelper.Send(resetPasswordData.Email, subject, body, _configuration);
            
            return Ok("Check your email");
        }

        [Route("~/[controller]/ConfirmReseting")]
        [HttpGet]
        public IActionResult ConfirmResetPassword(string email, string token)
        {
            token = token.Replace(" ", "+");
            return Ok(new ResetPasswordData
            {
                Email = email,
                Token = token
            });
        }

        [Route("~/[controller]/ConfirmNewPassword")]
        [HttpPost]
        public async Task<IActionResult> ConfirmNewPassword([FromBody] ResetPasswordData user)
        {
            user.Token = user.Token.Replace(" ", "+");
            await _accountService.ConfirmNewPassword(user.Email, user.Token, user.Password);
            return Ok("Password has successfully changed");
        }

        [Route("~/RefreshToken")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RefreshTokens([FromHeader]string Authorization, [FromHeader]string ipfingerprint)
        {
            string token = Authorization.Substring(7); //Remove 'Bearer ' from token
            var userModel = await _accountService.GetUserById(JwtHelper.GetUserIdFromToken(token));

            if (userModel.Errors.Count > 0)
                return NotFound(userModel.Errors);

            var user = userModel.Users[0];

            if (await _accountService.IsAccountLocked(user.Username) ||
                !await _accountService.CheckAndRemoveRefreshToken(user.Username, ipfingerprint, token))
            {
                return Unauthorized();
            }

            var refreshToken = JwtHelper.GenerateJwtRefreshToken(user, _configuration);
            await _accountService.SaveRefreshToken(user.Username, ipfingerprint, refreshToken);

            return Ok(new AccessTokenData
            {
                AccessToken = JwtHelper.GenerateJwtAccessToken(user, _configuration),
                RefreshToken = refreshToken
            });
        }
    }
}