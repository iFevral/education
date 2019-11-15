using System.Threading.Tasks;
using Store.BusinessLogic.Models.Users;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Return all users
        /// </summary>
        /// <returns>User model</returns>
        public Task<UserModel> GetAllUsers();

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
        /// Check received token for email confirmation
        /// </summary>
        /// <returns>True if token for email confirmation is valid</returns>
        public Task<bool> ConfirmEmail(string username, string token);
        /// <summary>
        /// Check user from repository and create token for password reset
        /// </summary>
        /// <returns>Token for password reset</returns>
        public Task<string> ResetPassword(string username);

        /// <summary>
        /// Check received token for new password confirmation
        /// </summary>
        /// <returns>True if token for new password confirmation is valid</returns>
        public Task<bool> ConfirmNewPassword(string username, string token, string newPassword);
    }
}
