using System.Collections.Generic;
using System.Threading.Tasks;
using Store.BusinessLogic.Models.Users;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Return all users
        /// </summary>
        /// <returns>User model</returns>
        public Task<UserModel> GetAllUsers();
        public Task<UserModelItem> GetUser(string username);
        public Task CreateUser(UserModelItem user);
        public Task EditUser(UserModelItem user);
        public Task DeleteUser(UserModelItem user);
        public Task AddUserToRole(string username, string role);
        public Task RemoveUserFromRole(string username, string role);
        public Task CreateRole(string rolename);
        public Task RemoveRole(string rolename);

    }
}