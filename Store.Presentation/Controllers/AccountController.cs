using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Store.Presentation.Helpers;
using Store.BusinessLogic.Helpers;
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
                                 IAccountService accountService)
        {
            _configuration = configuration;
            _accountService = accountService;
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
        public async Task<IActionResult> GetProfile([FromHeader]string Authorization)
        {
            //Remove "Bearer " from token
            string token = Authorization.Substring(7);

            var userModel = await _accountService.GetUserByIdAsync(JwtHelper.GetUserIdFromToken(token));

            if (userModel.Errors.Count > 0)
            {
                return NotFound(userModel);
            }

            return Ok(userModel);

        }

        [Route("~/[controller]/SignIn")]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInModel loginData)
        {
            var userModel = await _accountService.SignInAsync(loginData);

            if (userModel.Errors.Count > 0)
            {
                return NotFound(userModel);
            }

            if (await _accountService.IsAccountLockedAsync(userModel.Username))
            {
                return Unauthorized("User is blocked");
            }

            var refreshToken = JwtHelper.GenerateJwtRefreshToken(userModel, _configuration);

            var tokenModel = new AccessTokenModel();
            tokenModel.AccessToken = JwtHelper.GenerateJwtAccessToken(userModel, _configuration);
            tokenModel.RefreshToken = refreshToken;

            return Ok(tokenModel);
        }

        [Route("~/[controller]/SignUp")]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            var emailModel = await _accountService.SignUpAsync(signUpModel);

            if (emailModel.Errors.Count > 0)
            {
                return BadRequest(emailModel);
            }

            string subject = "Account confirmation";
            string body = "Confirmation link: <a href='https://localhost:44312/Account/ConfirmEmail?" +
                            "username=" + emailModel.Email + "&token=" + emailModel.Token + "'>Verify email</a>";
            
            EmailHelper.Send(signUpModel.Email, subject, body, _configuration);
            
            return Ok("Check your email to confirm your information");
        }

        [Route("~/[controller]/SignOut")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            return Ok("Sign out success");
        }


        [Route("~/[controller]/ConfirmEmail")]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string username, string token)
        {
            token = token.Replace(" ", "+");
            var userModel = await _accountService.ConfirmEmailAsync(username, token);
            if (userModel.Errors.Count > 0)
            {
                return BadRequest("Link not available");
            }

            return Ok("Email has successfully confirmed");
        }

        [Route("~/[controller]/ForgotPassword")]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromBody] EmailModel user)
        { 

            var resetPasswordModel = await _accountService.ResetPasswordAsync(user.Email);
            string subject = "Reseting password confirmation";
            string body = "Confirmation link: <a href='" + _configuration["Url"] + "/Account/ConfirmReseting?" +
                          "email=" + resetPasswordModel.Email + "&token=" + resetPasswordModel.Token + "'>Reset password</a>";
            
            EmailHelper.Send(resetPasswordModel.Email, subject, body, _configuration);
            
            return Ok("Check your email");
        }

        [Route("~/[controller]/ConfirmReseting")]
        [HttpGet]
        public IActionResult ConfirmResetPassword(string email, string token)
        {
            token = token.Replace(" ", "+");
            return Ok(new ResetPasswordModel
            {
                Email = email,
                Token = token
            });
        }

        [Route("~/[controller]/ConfirmNewPassword")]
        [HttpPost]
        public async Task<IActionResult> ConfirmNewPassword([FromBody] ResetPasswordModel user)
        {
            user.Token = user.Token.Replace(" ", "+");
            await _accountService.ConfirmNewPasswordAsync(user.Email, user.Token, user.Password);
            return Ok("Password has successfully changed");
        }

        [Route("~/RefreshToken")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RefreshTokens([FromHeader]string Authorization)
        {
            string token = Authorization.Substring(7); //Remove 'Bearer ' from token
            var userModel = await _accountService.GetUserByIdAsync(JwtHelper.GetUserIdFromToken(token));

            if (userModel.Errors.Count > 0)
            {
                return NotFound(userModel.Errors);
            }

            var refreshToken = JwtHelper.GenerateJwtRefreshToken(userModel, _configuration);

            return Ok(new AccessTokenModel
            {
                AccessToken = JwtHelper.GenerateJwtAccessToken(userModel, _configuration),
                RefreshToken = refreshToken
            });
        }
    }
}