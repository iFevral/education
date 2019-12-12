using System.Threading.Tasks;
using System.Collections.Generic;
using Store.DataAccess.Entities;
using Store.DataAccess.Models.EFFilters;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Return all users
        /// </summary>
        /// <returns>User entity</returns>
        public IEnumerable<User> GetAll(FilterModel<User> filterModel, out int counter);

        /// <summary>
        /// Create new user
        /// </summary>
        /// <returns>Enumerable of users</returns>
        public Task<bool> CreateAsync(User user, string password);

        /// <summary>
        /// Edit user
        /// </summary>
        public Task<bool> UpdateAsync(User user, string currentPassword, string newPassword);

        /// <summary>
        /// Delete user
        /// </summary>
        public Task<bool> RemoveAsync(User user);

        /// <summary>
        /// Lock user
        /// </summary>
        public Task<bool> LockAsync(string email);

        /// <summary>
        /// Unlock user
        /// </summary>
        public Task<bool> UnlockAsync(string email);

        /// <summary>
        /// Get all roles of user
        /// </summary>
        public Task<IList<string>> GetUserRolesAsync(long id);

        /// <summary>
        /// Find user by id
        /// </summary>
        /// <returns>User entity</returns>
        public Task<User> FindByIdAsync(long id);

        /// <summary>
        /// Find user by email
        /// </summary>
        /// <returns>User entity</returns>
        public Task<User> FindByEmailAsync(string email);

        /// <summary>
        /// Check that user is created
        /// </summary>
        /// <returns>True if user is found</returns>
        public Task<bool> CheckSignInAsync(string email, string password);

        /// <summary>
        /// Generate token for email confirmation
        /// </summary>
        /// <returns>Token for registration</returns>
        public Task<string> GenerateEmailConfirmationTokenAsync(string email);

        /// <summary>
        /// Confirm email
        /// </summary>
        public Task<bool> ConfirmEmailAsync(string email, string token);

        /// <summary>
        /// Check email confirmation token
        /// </summary>
        /// <returns>True if registration token is correct</returns>
        public Task<bool> CheckEmailConfirmationAsync(string email);

        /// <summary>
        /// Generate token for password reseting
        /// </summary>
        /// <returns>Token for password reset</returns>
        public Task<string> GeneratePasswordResetTokenAsync(string email);

        /// <summary>
        /// Check password token and set new password
        /// </summary>
        /// <returns>True if token for password reset is correct</returns>
        public Task<bool> ResetPasswordAsync(string email, string token, string newPassword);

        /// <summary>
        /// Add user from role
        /// </summary>
        public Task<bool> AddToRoleAsync(long id, string rolename);
    }
}
