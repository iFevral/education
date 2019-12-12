using System.Threading.Tasks;
using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.Filters;
using Store.BusinessLogic.Models.Users;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>User model</returns>
        public Task<UserModel> GetAllUsersAsync(UserFilterModel userFilter);

        /// <summary>
        /// Get user by name
        /// </summary>
        /// <returns>User model</returns>
        public Task<UserModelItem> GetUserByEmailAsync(string email);

        /// <summary>
        /// Edit user
        /// </summary>
        public Task<BaseModel> UpdateUserAsync(SignUpModel signUpModel);

        /// <summary>
        /// Delete user
        /// </summary>
        public Task<BaseModel> DeleteUserAsync(string email);

        /// <summary>
        /// Block user
        /// </summary>
        public Task<BaseModel> SetLockingStatus(string email, bool enabled);
    }
}