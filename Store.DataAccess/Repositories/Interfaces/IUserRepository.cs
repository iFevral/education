using Store.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Create new role
        /// </summary>
        public Task CreateRole(string name);
        
        /// <summary>
        /// Delete role
        /// </summary>
        public Task DeleteRole(string name);
        
        /// <summary>
        /// Get all roles of user
        /// </summary>
        public Task<IEnumerable<string>> GetUserRoles(string id);
        
        /// <summary>
        /// Check that user has role
        /// </summary>
        public Task<bool> IsInRole(string id, string role);
        
        /// <summary>
        /// Add user from role
        /// </summary>
        public Task AddToRole(string id, string role);
        
        /// <summary>
        /// Remove user from role
        /// </summary>
        public Task RemoveFromRole(string id, string role);
        
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
        /// Check that user is created
        /// </summary>
        /// <returns>True if user is found</returns>
        public Task<bool> IsCreated(string username, string password);

        /// <summary>
        /// Generate token for email confirmation
        /// </summary>
        /// <returns>Token for registration</returns>
        public Task<string> GenerateRegistrationToken(string username);

        /// <summary>
        /// Check email confirmation token
        /// </summary>
        /// <returns>True if registration token is valid</returns>
        public Task<bool> ConfirmEmail(string username, string token);

        /// <summary>
        /// Generate token for password reseting
        /// </summary>
        /// <returns>Token for password reset</returns>
        public Task<string> GeneratePasswordResetToken(string username);

        /// <summary>
        /// Check password token and set new password
        /// </summary>
        /// <returns>True if token for password resetis valid</returns>
        public Task<bool> ConfirmNewPassword(string username, string token, string newPassword);
    }
}
