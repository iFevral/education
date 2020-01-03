using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Common.Constants;
using Store.BusinessLogic.Helpers.Interface;
using Store.Presentation.Helpers.Interface;
using Store.DataAccess.Entities.Enums;
using System;

namespace Store.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailHelper _emailHelper;
        private readonly IJwtHelper _jwtHelper;
        private readonly IConfigurationSection _jwtConfig;
        private readonly IAccountService _accountService;
        public AccountController(IConfiguration configuration,
                                 IAccountService accountService,
                                 IEmailHelper emailHelper,
                                 IJwtHelper jwtHelper)
        {
            _configuration = configuration;
            _accountService = accountService;
            _emailHelper = emailHelper;
            _emailHelper.Configure(_configuration.GetSection("SMTP"));
            _jwtHelper = jwtHelper;
            _jwtConfig = _configuration.GetSection("JWT");
        }

        [Authorize(Roles = Constants.RoleNames.Admin + "," + Constants.RoleNames.Client)]
        [HttpPost]
        public async Task<IActionResult> Profile([FromHeader]string authorization)
        {
            long userId = _jwtHelper.GetUserIdFromToken(authorization);
            var userModel = await _accountService.GetUserByIdAsync(userId);

            return Ok(userModel);
        }

        [Route("~/[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> SignIn(bool isRememberMeActivated, [FromBody] SignInModel loginData)
        {
            var userModel = await _accountService.SignInAsync(loginData);
            var tokenModel = new TokenModel();

            if (userModel.Errors.Any())
            {
                tokenModel.Errors = userModel.Errors;

                return Ok(tokenModel);
            }


            double tokenLifeTime = _jwtConfig.GetValue<double>("AccessTokenLifeTime");
            string secretKey = _jwtConfig.GetValue<string>("SecretKey");


            tokenModel.AccessToken = _jwtHelper.GenerateToken(userModel, tokenLifeTime, secretKey, true);


            tokenLifeTime = isRememberMeActivated
                ? _jwtConfig.GetValue<double>("RefreshTokenLifeTimeLong")
                : _jwtConfig.GetValue<double>("RefreshTokenLifeTime");


            tokenModel.RefreshToken = _jwtHelper.GenerateToken(userModel, tokenLifeTime, secretKey, false);

            return Ok(tokenModel);
        }

        [Route("~/[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            var emailConfirmationModel = await _accountService.SignUpAsync(signUpModel);

            if (emailConfirmationModel.Errors.Any())
            {
                return Ok(emailConfirmationModel);
            }

            string subject = Constants.EmailHeaders.EmailConfirmation;
            string body = $"Confirmation link: <a href='{_configuration["ViewUrl"]}/Account/ConfirmEmail?email={emailConfirmationModel.Email}&token={emailConfirmationModel.Token}'>Verify Email</a>";

            await _emailHelper.Send(signUpModel.Email, subject, body);
            emailConfirmationModel.Token = null;
            emailConfirmationModel.Message = $"Check your email {emailConfirmationModel.Email} for confirmation";

            return Ok(emailConfirmationModel);
        }

        [Authorize(Roles = Constants.RoleNames.Admin + "," + Constants.RoleNames.Client)]
        [HttpPatch]
        public async Task<IActionResult> Edit([FromHeader]string authorization, [FromBody] SignUpModel signUpModel)
        {
            signUpModel.Id = _jwtHelper.GetUserIdFromToken(authorization);
            var model = await _accountService.UpdateProfile(signUpModel);

            return Ok(model);
        }


        [Route("~/[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> ConfirmEmail([FromBody]EmailConfirmationModel emailConfirmationModel)
        {
            emailConfirmationModel.Token = emailConfirmationModel.Token.Replace(" ", "+");
            var model = await _accountService.ConfirmEmailAsync(emailConfirmationModel);

            return Ok(model);
        } 

        [Route("~/[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromBody] EmailConfirmationModel user)
        {

            var resetPasswordModel = await _accountService.ResetPasswordAsync(user.Email);
            string subject = Constants.EmailHeaders.ResetingPasswordConfirmation;
            string body = $"New password: {resetPasswordModel.Password}. You can change it in account settings";

            await _emailHelper.Send(resetPasswordModel.Email, subject, body);

            return Ok(resetPasswordModel);
        }

        [Route("~/[controller]/[action]")]
        [Authorize(Roles = Constants.RoleNames.Admin + "," + Constants.RoleNames.Client)]
        [HttpPost]
        public async Task<IActionResult> GetRole([FromHeader] string authorization)
        {
            string roleName = _jwtHelper.GetUserRoleFromToken(authorization);
            Enum.TryParse(roleName, out Enums.Role.RoleName role);

            return Ok(role);
        }

        [Route("~/[action]")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RefreshToken(bool isRememberMeActivated, [FromHeader]string authorization)
        {
            var userModel = await _accountService.GetUserByIdAsync(_jwtHelper.GetUserIdFromToken(authorization));

            if (userModel.Errors.Any())
            {
                return Ok(userModel);
            }

            var tokenModel = new TokenModel();

            double tokenLifeTime = _jwtConfig.GetValue<double>("AccessTokenLifeTime");
            string secretKey = _jwtConfig.GetValue<string>("SecretKey");

            tokenModel.AccessToken = _jwtHelper.GenerateToken(userModel, tokenLifeTime, secretKey, true);

            tokenLifeTime = isRememberMeActivated
                ? _jwtConfig.GetValue<double>("RefreshTokenLifeTimeLong")
                : _jwtConfig.GetValue<double>("RefreshTokenLifeTime");

            tokenModel.RefreshToken = _jwtHelper.GenerateToken(userModel, tokenLifeTime, secretKey, false);

            return Ok(tokenModel);
        }
    }
}