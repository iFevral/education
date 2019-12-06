using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Store.Presentation.Helpers;
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
        [Authorize(Roles = Constants.RoleNames.Admin + "," + Constants.RoleNames.Client)]
        [HttpPost]
        public async Task<IActionResult> GetProfileAsync([FromHeader]string Authorization)
        {
            string token = Authorization.Substring(7);
            long userId = JwtHelper.GetUserIdFromToken(token);
            var userModel = await _accountService.GetUserByIdAsync(userId);

            return Ok(userModel);

        }

        [Route("~/[controller]/SignIn")]
        [HttpPost]
        public async Task<IActionResult> SignInAsync([FromBody] SignInModel loginData)
        {
            var userModel = await _accountService.SignInAsync(loginData);

            if (userModel.Errors.Count > 0)
            {
                return Ok(userModel);
            }

            var refreshToken = JwtHelper.GenerateJwtRefreshToken(userModel, _configuration);

            var tokenModel = new AccessTokenModel();
            tokenModel.AccessToken = JwtHelper.GenerateJwtAccessToken(userModel, _configuration);
            tokenModel.RefreshToken = refreshToken;

            return Ok(tokenModel);
        }

        [Route("~/[controller]/SignUp")]
        [HttpPost]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpModel signUpModel)
        {
            var emailConfirmationModel = await _accountService.SignUpAsync(signUpModel);

            if (emailConfirmationModel.Errors.Count > 0)
            {
                return Ok(emailConfirmationModel);
            }

            string subject = "Account confirmation";
            string body = "Confirmation link: <a href='https://localhost:44312/Account/ConfirmEmail?" +
                            "username=" + emailConfirmationModel.Email + "&token=" + emailConfirmationModel.Token + "'>Verify email</a>";

            await _emailHelper.Send(signUpModel.Email, subject, body);

            return Ok(emailConfirmationModel);
        }

        [Route("~/[controller]/Edit")]
        [HttpPost]
        public async Task<IActionResult> EditProfileAsync([FromHeader]string Authorization, [FromBody] SignUpModel signUpModel)
        {
            var token = Authorization.Substring(7);
            signUpModel.Id = JwtHelper.GetUserIdFromToken(token);
            var model = await _accountService.UpdateProfile(signUpModel);

            return Ok(model);
        }


        [Route("~/[controller]/ConfirmEmail")]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmailAsync(string username, string token)
        {
            token = token.Replace(" ", "+");
            var userModel = await _accountService.ConfirmEmailAsync(username, token);

            return Ok(userModel);
        }

        [Route("~/[controller]/ForgotPassword")]
        [HttpPost]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] EmailConfirmationModel user)
        {

            var resetPasswordModel = await _accountService.GeneratePasswordResetTokenAsync(user.Email);
            string subject = "Reseting password confirmation";
            string body = "Confirmation link: <a href='" + _configuration["Url"] + "/Account/ConfirmReseting?" +
                          "email=" + resetPasswordModel.Email + "&token=" + resetPasswordModel.Token + "'>Reset password</a>";

            await _emailHelper.Send(resetPasswordModel.Email, subject, body);

            return Ok(resetPasswordModel);
        }

        [Route("~/[controller]/ConfirmReseting")]
        [HttpGet]
        public async Task<IActionResult> ConfirmResetPasswordAsync(string email, string token)
        {
            token = token.Replace(" ", "+");
            var resetPasswordModel = new ResetPasswordModel();
            
            resetPasswordModel.Email = email;
            resetPasswordModel.Token = token;
            
            return Ok(resetPasswordModel);
        } 

        [Route("~/[controller]/ConfirmNewPassword")]
        [HttpPost]
        public async Task<IActionResult> ConfirmNewPasswordAsync([FromBody] ResetPasswordModel user)
        {
            user.Token = user.Token.Replace(" ", "+");
            
            var model = await _accountService.ResetPasswordAsync(user.Email, user.Token, user.Password);
            
            return Ok(model);
        }

        [Route("~/RefreshToken")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RefreshTokensAsync([FromHeader]string Authorization)
        {
            string token = Authorization.Substring(7);
            var userModel = await _accountService.GetUserByIdAsync(JwtHelper.GetUserIdFromToken(token));

            if (userModel.Errors.Count > 0)
            {
                return Ok(userModel.Errors);
            }

            var refreshToken = JwtHelper.GenerateJwtRefreshToken(userModel, _configuration);
            
            var accessTokenModel = new AccessTokenModel();
            accessTokenModel.AccessToken = JwtHelper.GenerateJwtAccessToken(userModel, _configuration);
            accessTokenModel.RefreshToken = refreshToken;

            return Ok(accessTokenModel);
        }
    }
}