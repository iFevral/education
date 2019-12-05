using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Store.Presentation.Helpers;
using Store.BusinessLogic.Helpers;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Common;
using Store.BusinessLogic.Helpers.Interface;

namespace Store.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;
        private readonly IEmailHelper _emailHelper;
        public AccountController(IConfiguration configuration,
                                 IAccountService accountService,
                                 IEmailHelper emailHelper)
        {
            _configuration = configuration;
            _accountService = accountService;
            _emailHelper = emailHelper;
            _emailHelper.Configure(_configuration.GetSection("SMTP"));
        }

        [Route("~/[controller]/Profile")]
        [Authorize(Roles = Constants.RoleNames.Client)]
        [Authorize(Roles = Constants.RoleNames.Admin)]
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
            
            await _emailHelper.Send(signUpModel.Email, subject, body);
            
            return Ok("Check your email to confirm your information");
        }

        [Route("~/[controller]/SignOut")]
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
        public async Task<IActionResult> ForgotPassword([FromBody] EmailConfirmationModel user)
        { 

            var resetPasswordModel = await _accountService.GeneratePasswordResetTokenAsync(user.Email);
            string subject = "Reseting password confirmation";
            string body = "Confirmation link: <a href='" + _configuration["Url"] + "/Account/ConfirmReseting?" +
                          "email=" + resetPasswordModel.Email + "&token=" + resetPasswordModel.Token + "'>Reset password</a>";

            await _emailHelper.Send(resetPasswordModel.Email, subject, body);
            
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
            await _accountService.ResetPasswordAsync(user.Email, user.Token, user.Password);
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