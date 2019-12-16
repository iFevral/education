using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Common;
using Store.BusinessLogic.Helpers.Interface;
using Store.Presentation.Helpers.Interface;

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
            string token = authorization.Substring(7);
            long userId = _jwtHelper.GetUserIdFromToken(token);
            var userModel = await _accountService.GetUserByIdAsync(userId);

            return Ok(userModel);

        }

        [Route("~/[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInModel loginData)
        {
            var userModel = await _accountService.SignInAsync(loginData);

            if (userModel.Errors.Any())
            {
                return Ok(userModel);
            }

            var tokenModel = new TokenModel();

            tokenModel.AccessToken = _jwtHelper.GenerateToken(userModel,
                                                                _jwtConfig.GetValue<double>("AccessTokenExpireMinutes"),
                                                                _jwtConfig.GetValue<string>("SecretKey"),
                                                                true);
            tokenModel.RefreshToken = _jwtHelper.GenerateToken(userModel,
                                                                _jwtConfig.GetValue<double>("RefreshTokenExpireMinutes"),
                                                                _jwtConfig.GetValue<string>("SecretKey"),
                                                                false);

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
            string body = $"Confirmation link: <a href='{_configuration["Url"]}/Account/ConfirmEmail?email={emailConfirmationModel.Email}&token={emailConfirmationModel.Token}'>Verify Email</a>";

            await _emailHelper.Send(signUpModel.Email, subject, body);
            emailConfirmationModel.Token = null;
            emailConfirmationModel.Message = $"Check your email {emailConfirmationModel.Email} for confirmation";

            return Ok(emailConfirmationModel);
        }

        [HttpPatch]
        [Authorize(Roles = Constants.RoleNames.Admin + "," + Constants.RoleNames.Client)]
        public async Task<IActionResult> Edit([FromHeader]string Authorization, [FromBody] SignUpModel signUpModel)
        {
            var token = Authorization.Substring(7);
            signUpModel.Id = _jwtHelper.GetUserIdFromToken(token);
            var model = await _accountService.UpdateProfile(signUpModel);

            return Ok(model);
        }


        [Route("~/[controller]/[action]")]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            token = token.Replace(" ", "+");
            var userModel = await _accountService.ConfirmEmailAsync(email, token);

            return Ok(userModel);
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

        [Route("~/[action]")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromHeader]string authorization)
        {
            string token = authorization.Substring(7);
            var userModel = await _accountService.GetUserByIdAsync(_jwtHelper.GetUserIdFromToken(token));

            if (userModel.Errors.Any())
            {
                return Ok(userModel);
            }

            var tokenModel = new TokenModel();

            tokenModel.AccessToken = _jwtHelper.GenerateToken(userModel,
                                                                _jwtConfig.GetValue<double>("AccessTokenExpireMinutes"),
                                                                _jwtConfig.GetValue<string>("SecretKey"),
                                                                true);
            tokenModel.RefreshToken = _jwtHelper.GenerateToken(userModel,
                                                                _jwtConfig.GetValue<double>("RefreshTokenExpireMinutes"),
                                                                _jwtConfig.GetValue<string>("SecretKey"),
                                                                false);

            return Ok(tokenModel);
        }
    }
}