using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.DataAccess.Entities;
using Store.DataAccess.AppContext;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.Repositories.EFRepository;

namespace Store.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private IMapper _mapper;
        private IUserRepository _userRepository;
        public UserService(ApplicationContext db,
                              UserManager<Users> userManager,
                              RoleManager<Roles> roleManager,
                              SignInManager<Users> signInManager,
                              IMapper mapper)
        {
            _userRepository = new UserRepository(db, userManager, roleManager, signInManager);
            _mapper = mapper;
        }

        public async Task<UserModel> GetAllUsers()
        {
            UserModel users = new UserModel();
            var usersFromRepo = await _userRepository.GetAll();

            foreach (var user in usersFromRepo)
            {
                //Map from Users to UserModelItem
                users.Items.Add(_mapper.Map<UserModelItem>(user));
            }

            return users;
        }

        public async Task<UserModelItem> GetUserById(string id)
        {
            var user = _mapper.Map<UserModelItem>(await _userRepository.FindById(id));
            if (user != null)
                user.Roles = await _userRepository.GetUserRoles(user.UserName);
            
            return user;
        }

        public async Task<UserModelItem> GetUserByName(string username)
        {
            var user = _mapper.Map<UserModelItem>(await _userRepository.FindByName(username));
            if (user != null)
                user.Roles = await _userRepository.GetUserRoles(user.UserName);

            return user;
        }

        public async Task AddUserToRole(string username, string role)
        {
            var user = await _userRepository.FindByName(username);
            await _userRepository.AddToRole(user.Id,role);
        }

        public async Task CreateRole(string rolename)
        {
            throw new System.NotImplementedException();
        }

        public async Task CreateUser(UserModelItem user)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteUser(UserModelItem user)
        {
            throw new System.NotImplementedException();
        }

        public async Task EditUser(UserModelItem user)
        {
            throw new System.NotImplementedException();
        }

        public async Task RemoveRole(string rolename)
        {
            throw new System.NotImplementedException();
        }

        public async Task RemoveUserFromRole(string username, string role)
        {
            throw new System.NotImplementedException();
        }
    }
}
