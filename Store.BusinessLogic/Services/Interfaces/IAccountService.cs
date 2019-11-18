using System.Threading.Tasks;
using Store.BusinessLogic.Models.Users;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAccountService
    {

        /// <summary>
        /// Return user by username
        /// </summary>
        /// <returns>User model item</returns>
        public Task<UserModelItem> GetUser(string username);

        /// <summary>
        /// Check user login and password and get user data
        /// </summary>
        /// <returns>User model item</returns>
        public Task<UserModelItem> SignIn(SignInModelItem loginData);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<UserModelItem> SignOut();

        /// <summary>
        /// Create new user and generate token for registration
        /// </summary>
        /// <returns>Email token for registration</returns>
        public Task<string> SignUp(SignUpModelItem userData);

        /// <summary>
        /// Confirm email
        /// </summary>
        public Task<bool> ConfirmEmail(string username, string token);

        /// <summary>
        /// Check email confirmation
        /// </summary>
        /// <returns>True if email confirmed</returns>
        public Task<bool> IsEmailConfirmed(string username);

        /// <summary>
        /// Check user from repository and create token for password reset
        /// </summary>
        /// <returns>Token for password reset</returns>
        public Task<string> ResetPassword(string username);

        /// <summary>
        /// Set new password 
        /// </summary>
        public Task ConfirmNewPassword(string username, string token, string newPassword);
    }
}
