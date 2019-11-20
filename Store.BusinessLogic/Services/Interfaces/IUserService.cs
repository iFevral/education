using System.Collections.Generic;
using System.Threading.Tasks;
using Store.BusinessLogic.Models.Users;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>User model</returns>
        public Task<UserModel> GetAllUsers();

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <returns>User model</returns>
        public Task<UserModel> GetUserById(string id);

        /// <summary>
        /// Get user by name
        /// </summary>
        /// <returns>User model</returns>
        public Task<UserModel> GetUserByName(string username);

        /// <summary>
        /// Create user
        /// </summary>
        public Task CreateUser(SignUpData signUpData);

        /// <summary>
        /// Edit user
        /// </summary>
        public Task EditUser(SignUpData signUpData);

        /// <summary>
        /// Delete user
        /// </summary>
        public Task DeleteUser(string username);

        /// <summary>
        /// Block user
        /// </summary>
        public Task BlockUser(string username, bool enabled);

        /// <summary>
        /// Add user to role
        /// </summary>
        public Task AddUserToRole(string username, string rolename);

        /// <summary>
        /// Remove user from role
        /// </summary>
        /// <returns>User model</returns>
        public Task RemoveUserFromRole(string username, string rolename);

        /// <summary>
        /// Create role
        /// </summary>
        public Task CreateRole(string rolename);

        /// <summary>
        /// Remove role
        /// </summary>
        public Task RemoveRole(string rolename);
    }
}