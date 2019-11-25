using System.Threading.Tasks;
using Store.BusinessLogic.Models.Roles;
using Store.BusinessLogic.Models.Users;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>User model</returns>
        public UserModel GetAllUsers(UserFilter userFilter);

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
        public Task<UserModel> CreateUser(SignUpData signUpData);

        /// <summary>
        /// Edit user
        /// </summary>
        public Task<UserModel> EditUser(SignUpData signUpData);

        /// <summary>
        /// Delete user
        /// </summary>
        public Task<UserModel> DeleteUser(string username);

        /// <summary>
        /// Block user
        /// </summary>
        public Task<UserModel> BlockUser(string username, bool enabled);

        /// <summary>
        /// Add user to role
        /// </summary>
        /// <returns>UserRole model</returns>
        public Task<RoleModel> AddUserToRole(UserRoleModelItem userRoleModel);

        /// <summary>
        /// Remove user from role
        /// </summary>
        /// <returns>UserRole model</returns>
        public Task<RoleModel> RemoveUserFromRole(UserRoleModelItem userRoleModel);

        /// <summary>
        /// Create role
        /// </summary>
        public Task<RoleModel> CreateRole(RoleModelItem role);

        /// <summary>
        /// Remove role
        /// </summary>
        public Task<RoleModel> RemoveRole(RoleModelItem role);
    }
}