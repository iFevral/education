using Store.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Return all users
        /// </summary>
        /// <returns>User entity</returns>
        public Task<IEnumerable<Users>> GetAllAsync();

        /// <summary>
        /// Create new user
        /// </summary>
        /// <returns>Enumerable of users</returns>
        public Task<bool> CreateAsync(Users user, string password);

        /// <summary>
        /// Edit user
        /// </summary>
        public Task<bool> UpdateAsync(Users user);

        /// <summary>
        /// Delete user
        /// </summary>
        public Task<bool> RemoveAsync(Users user);

        /// <summary>
        /// Lock user
        /// </summary>
        public Task<bool> LockOutAsync(string username, bool enabled);

        /// <summary>
        /// Check if user locked
        /// </summary>
        /// <returns>True if user locked</returns>
        public Task<bool> IsLockedOutAsync(string username);

        /// <summary>
        /// Check if role created
        /// </summary>
        /// <returns>True if role created</returns>
        public Task<bool> CheckRoleAvailabilityAsync(string rolename);

        /// <summary>
        /// Create new role
        /// </summary>
        public Task<bool> CreateRoleAsync(string rolename);

        /// <summary>
        /// Delete role
        /// </summary>
        public Task<bool> DeleteRoleAsync(string rolename);

        /// <summary>
        /// Get all roles of user
        /// </summary>
        public Task<IList<string>> GetUserRolesAsync(string id);

        /// <summary>
        /// Check that user has role
        /// </summary>
        public Task<bool> CheckRoleAsync(string id, string rolename);

        /// <summary>
        /// Add user from role
        /// </summary>
        public Task<bool> AddToRoleAsync(string id, string rolename);

        /// <summary>
        /// Remove user from role
        /// </summary>
        public Task<bool> RemoveFromRoleAsync(string id, string rolename);

        /// <summary>
        /// Find user by id
        /// </summary>
        /// <returns>User entity</returns>
        public Task<Users> FindByIdAsync(string id);

        /// <summary>
        /// Find user by email
        /// </summary>
        /// <returns>User entity</returns>
        public Task<Users> FindByEmailAsync(string email);

        /// <summary>
        /// Find user by name
        /// </summary>
        /// <returns>User entity</returns>
        public Task<Users> FindByNameAsync(string username);

        /// <summary>
        /// Check that user is created
        /// </summary>
        /// <returns>True if user is found</returns>
        public Task<bool> CheckSignInAsync(string username, string password);

        /// <summary>
        /// Generate token for email confirmation
        /// </summary>
        /// <returns>Token for registration</returns>
        public Task<string> GenerateEmailConfirmationTokenAsync(string username);

        /// <summary>
        /// Confirm email
        /// </summary>
        public Task<bool> ConfirmEmailAsync(string username, string token);

        /// <summary>
        /// Check email confirmation token
        /// </summary>
        /// <returns>True if registration token is correct</returns>
        public Task<bool> CheckEmailConfirmationAsync(string username);

        /// <summary>
        /// Generate token for password reseting
        /// </summary>
        /// <returns>Token for password reset</returns>
        public Task<string> GeneratePasswordResetTokenAsync(string username);

        /// <summary>
        /// Check password token and set new password
        /// </summary>
        /// <returns>True if token for password reset is correct</returns>
        public Task<bool> ConfirmNewPasswordAsync(string username, string token, string newPassword);
    }
}
