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
        public Task<UserModel> GetAllUsersAsync(UserFilter userFilter);

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <returns>User model</returns>
        public Task<UserModelItem> GetUserByIdAsync(string id);

        /// <summary>
        /// Get user by name
        /// </summary>
        /// <returns>User model</returns>
        public Task<UserModelItem> GetUserByNameAsync(string username);

        /// <summary>
        /// Create user
        /// </summary>
        public Task<SignUpModel> CreateUserAsync(SignUpModel signUpModel);

        /// <summary>
        /// Edit user
        /// </summary>
        public Task<UserModelItem> UpdateUserAsync(SignUpModel signUpModel);

        /// <summary>
        /// Delete user
        /// </summary>
        public Task<UserModelItem> DeleteUserAsync(string username);

        /// <summary>
        /// Block user
        /// </summary>
        public Task<UserModelItem> BlockUserAsync(string username, bool enabled);

        /// <summary>
        /// Add user to role
        /// </summary>
        public Task<UserRoleModelItem> AddUserToRoleAsync(UserRoleModelItem userRoleModel);

        /// <summary>
        /// Remove user from role
        /// </summary>
        /// <returns>User model</returns>
        public Task<UserRoleModelItem> RemoveUserFromRoleAsync(UserRoleModelItem userRoleModel);

        /// <summary>
        /// Create role
        /// </summary>
        public Task<RoleModelItem> CreateRoleAsync(RoleModelItem roleModel);

        /// <summary>
        /// Remove role
        /// </summary>
        public Task<RoleModelItem> RemoveRoleAsync(RoleModelItem roleModel);
    }
}