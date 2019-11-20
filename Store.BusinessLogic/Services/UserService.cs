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
            UserModel userModel = new UserModel();
            var usersFromRepo = await _userRepository.GetAll();
            
            if(usersFromRepo == null)
            {
                userModel.Errors.Add("No users found");
                return userModel;
            }


            foreach (var user in usersFromRepo)
            {
                //Map from Users to UserModelItem
                userModel.Users.Add(_mapper.Map<UserModelItem>(user));
            }

            return userModel;
        }

        public async Task<UserModel> GetUserById(string id)
        {
            var userModel = new UserModel();
            var user = _mapper.Map<UserModelItem>(await _userRepository.FindById(id));
            if (user == null)
            {
                userModel.Errors.Add("User not found");
                return userModel;
            }

            user.Roles = await _userRepository.GetUserRoles(user.Username);

            userModel.Users.Add(user);
            return userModel;
        }

        public async Task<UserModel> GetUserByName(string username)
        {
            var userModel = new UserModel();
            var user = _mapper.Map<UserModelItem>(await _userRepository.FindByName(username));
            
            if (user == null)
            {
                userModel.Errors.Add("User not found");
                return userModel;
            }

            user.Roles = await _userRepository.GetUserRoles(user.Username);

            userModel.Users.Add(user);
            return userModel;
        }

        public async Task CreateUser(SignUpData signUpData)
        {
            if (await _userRepository.FindByName(signUpData.Username) != null)
            {
                throw new System.Exception();
            }

            //Create user 
            await _userRepository.Create(_mapper.Map<Users>(signUpData), signUpData.Password);

            //Find user
            var user = await _userRepository.FindByName(signUpData.Username);

            //Add user to role
            await _userRepository.AddToRole(user.Id, "Client");

            //Unblock
            await _userRepository.LockOut(user.UserName, false);
        }

        public async Task EditUser(SignUpData signUpData)
        {
            var user = await _userRepository.FindByEmail(signUpData.Email);
            user.FirstName = signUpData.Firstname;
            user.LastName = signUpData.Lastname;

            await _userRepository.Update(user);
        }

        public async Task DeleteUser(string username)
        {
            var user = await _userRepository.FindByName(username);
            await _userRepository.Remove(user);
        }

        public async Task BlockUser(string username, bool enabled)
        {
            await _userRepository.LockOut(username, enabled);
        }

        public async Task CreateRole(string rolename)
        {
            await _userRepository.CreateRole(rolename);
        }

        public async Task RemoveRole(string rolename)
        {
            await _userRepository.DeleteRole(rolename);

        }

        public async Task AddUserToRole(string username, string rolename)
        {
            var user = await _userRepository.FindByName(username);
            await _userRepository.AddToRole(user.Id, rolename);
        }

        public async Task RemoveUserFromRole(string username, string rolename)
        {
            var user = await _userRepository.FindByName(username);
            await _userRepository.RemoveFromRole(user.Id, rolename);
        }
    }
}
