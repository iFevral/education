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
        public Task<IEnumerable<Users>> GetAll();

        /// <summary>
        /// Create new user
        /// </summary>
        /// <returns>Enumerable of users</returns>
        public Task Create(Users user, string password);

        /// <summary>
        /// Edit user
        /// </summary>
        public Task Update(Users user);

        /// <summary>
        /// Delete user
        /// </summary>
        public Task Remove(Users user);

        /// <summary>
        /// Lock user
        /// </summary>
        public Task LockOut(string username, bool enabled);

        /// <summary>
        /// Check if user locked
        /// </summary>
        /// <returns>True if user locked</returns>
        public Task<bool> IsLockedOut(string username);

        /// <summary>
        /// Check if role created
        /// </summary>
        /// <returns>True if role created</returns>
        public Task<bool> IsRoleCreated(string rolename);

        /// <summary>
        /// Create new role
        /// </summary>
        public Task CreateRole(string rolename);
        
        /// <summary>
        /// Delete role
        /// </summary>
        public Task DeleteRole(string rolename);
        
        /// <summary>
        /// Get all roles of user
        /// </summary>
        public Task<IEnumerable<string>> GetUserRoles(string id);
        
        /// <summary>
        /// Check that user has role
        /// </summary>
        public Task<bool> IsInRole(string id, string rolename);
        
        /// <summary>
        /// Add user from role
        /// </summary>
        public Task AddToRole(string id, string rolename);
        
        /// <summary>
        /// Remove user from role
        /// </summary>
        public Task RemoveFromRole(string id, string rolename);
        
        /// <summary>
        /// Find user by id
        /// </summary>
        /// <returns>User entity</returns>
        public Task<Users> FindById(string id);

        /// <summary>
        /// Find user by email
        /// </summary>
        /// <returns>User entity</returns>
        public Task<Users> FindByEmail(string email);

        /// <summary>
        /// Find user by name
        /// </summary>
        /// <returns>User entity</returns>
        public Task<Users> FindByName(string username);

        /// <summary>
        /// Check that user is created
        /// </summary>
        /// <returns>True if user is found</returns>
        public Task<bool> IsLoginDataCorrect(string username, string password);

        /// <summary>
        /// Generate token for email confirmation
        /// </summary>
        /// <returns>Token for registration</returns>
        public Task<string> GenerateEmailConfirmationToken(string username);

        /// <summary>
        /// Confirm email
        /// </summary>
        public Task<bool> ConfirmEmail(string username, string token);
        
        /// <summary>
        /// Check email confirmation token
        /// </summary>
        /// <returns>True if registration token is correct</returns>
        public Task<bool> IsEmailConfirmed(string username);

        /// <summary>
        /// Generate token for password reseting
        /// </summary>
        /// <returns>Token for password reset</returns>
        public Task<string> GeneratePasswordResetToken(string username);

        /// <summary>
        /// Check password token and set new password
        /// </summary>
        /// <returns>True if token for password reset is correct</returns>
        public Task ConfirmNewPassword(string username, string token, string newPassword);

        /// <summary>
        /// Get refresh token
        /// </summary>
        /// <returns>Refresh token</returns>
        public Task<string> GetRefreshToken(string userId, string fingerprint);

        /// <summary>
        /// Save refresh token
        /// </summary>
        public Task SaveRefreshToken(string username, string fingerprint, string newToken);

        /// <summary>
        /// Remove refresh token
        /// </summary>
        public Task RemoveRefreshToken(string username, string ipfingerprint);
    }
}
