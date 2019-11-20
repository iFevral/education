using System.Threading.Tasks;
using Store.BusinessLogic.Models.Users;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAccountService
    {
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <returns>User model item</returns>
        public Task<UserModel> GetUserById(string id);

        /// <summary>
        /// Get user by username
        /// </summary>
        /// <returns>User model item</returns>
        public Task<UserModel> GetUserByName(string username);

        /// <summary>
        /// Check user login and password and get user data
        /// </summary>
        /// <returns>User model item</returns>
        public Task<UserModel> SignIn(SignInData loginData);

        /// <summary>
        /// Create new user and generate token for registration
        /// </summary>
        /// <returns>Email token for registration</returns>
        public Task<UserModel> SignUp(SignUpData userData);

        /// <summary>
        /// Confirm email
        /// </summary>
        public Task<bool> ConfirmEmail(string username, string token);

        /// <summary>
        /// Check if user is blocked
        /// </summary>
        /// <returns>True if user is blocked</returns>
        public Task<bool> IsAccountLocked(string username);

        /// <summary>
        /// Check user from repository and create token for password reset
        /// </summary>
        /// <returns>Token for password reset</returns>
        public Task<UserModel> ResetPassword(string username);

        /// <summary>
        /// Set new password 
        /// </summary>
        public Task ConfirmNewPassword(string email, string token, string newPassword);

        /// <summary>
        /// Check if token correct and remove from database
        /// </summary>
        /// <returns>True if token correct</returns>
        public Task<bool> CheckAndRemoveRefreshToken(string username, string ipfingerprint, string token);

        /// <summary>
        /// Save refresh token in database
        /// </summary>
        public Task SaveRefreshToken(string username, string ipfingerprint, string newToken);
    }
}
