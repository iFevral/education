using System.Threading.Tasks;
using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.Users;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAccountService
    {
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <returns>User model item</returns>
        public Task<UserModelItem> GetUserByIdAsync(string id);

        /// <summary>
        /// Check user login and password and get user data
        /// </summary>
        /// <returns>User model item</returns>
        public Task<UserModelItem> SignInAsync(SignInModel signInModel);

        /// <summary>
        /// Create new user and generate token for registration
        /// </summary>
        /// <returns>Email token for registration</returns>
        public Task<EmailConfirmationModel> SignUpAsync(SignUpModel signUpModel);

        /// <summary>
        /// Confirm email
        /// </summary>
        public Task<BaseModel> ConfirmEmailAsync(string username, string token);

        /// <summary>
        /// Check user from repository and create token for password reset
        /// </summary>
        /// <returns>Token for password reset</returns>
        public Task<ResetPasswordModel> GeneratePasswordResetTokenAsync(string username);

        /// <summary>
        /// Set new password 
        /// </summary>
        public Task<ResetPasswordModel> ResetPasswordAsync(string email, string token, string newPassword);
    }
}
