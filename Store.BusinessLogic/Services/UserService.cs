using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.EFRepository;
using Store.DataAccess.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private UserModel _users;
        private IMapper _mapper;
        private IUserRepository _userRepository;
        public UserService(ApplicationContext db,
                              UserManager<Users> userManager,
                              RoleManager<Roles> roleManager,
                              SignInManager<Users> signInManager,
                              IMapper mapper)
        {
            _users = new UserModel();
            _userRepository = new UserRepository(db, userManager, roleManager, signInManager);
            _mapper = mapper;
        }

        public Task AddUserToRole(string username, string role)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateRole(string rolename)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateUser(UserModelItem user)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteUser(UserModelItem user)
        {
            throw new System.NotImplementedException();
        }

        public Task EditUser(UserModelItem user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserModel> GetAllUsers()
        {
            var users = await _userRepository.GetAll();

            foreach (var user in users)
            {
                //Map from Users to UserModelItem
                _users.Items.Add(_mapper.Map<UserModelItem>(user));
            }

            return _users;
        }

        public Task<UserModelItem> GetUser(string username)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveRole(string rolename)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveUserFromRole(string username, string role)
        {
            throw new System.NotImplementedException();
        }
    }
}
