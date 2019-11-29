using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.DataAccess.Entities;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.Repositories.EFRepository;
using Store.BusinessLogic.Models.Roles;
using Store.BusinessLogic.Common;

namespace Store.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private IMapper _mapper;
        private IUserRepository _userRepository;
        public UserService(UserManager<Users> userManager,
                           RoleManager<Roles> roleManager,
                           SignInManager<Users> signInManager,
                           IMapper mapper)
        {
            _userRepository = new UserRepository(userManager, roleManager, signInManager);
            _mapper = mapper;
        }

        public async Task<UserModel> GetAllUsersAsync()
        {
            UserModel userModel = new UserModel();
            var usersFromRepo = await _userRepository.GetAllAsync();

            if (usersFromRepo == null)
            {
                userModel.Errors.Add(Constants.ServiceValidationErrors.UsersNotExistError);
                return userModel;
            }


            foreach (var user in usersFromRepo)
            {
                //Map from Users to UserModelItem
                userModel.Users.Add(_mapper.Map<UserModelItem>(user));
            }

            return userModel;
        }

        public async Task<UserModelItem> GetUserByIdAsync(string id)
        {
            var user = _mapper.Map<UserModelItem>(await _userRepository.FindByIdAsync(id));
            if (user == null)
            {
                user = new UserModelItem();
                user.Errors.Add(Constants.ServiceValidationErrors.UserNotExistsError);
                return user;
            }

            user.Roles = await _userRepository.GetUserRolesAsync(user.Username);
            return user;
        }

        public async Task<UserModelItem> GetUserByNameAsync(string username)
        {
            var user = _mapper.Map<UserModelItem>(await _userRepository.FindByNameAsync(username));
            if (user == null)
            {
                user = new UserModelItem();
                user.Errors.Add(Constants.ServiceValidationErrors.UserNotExistsError);
                return user;
            }

            user.Roles = await _userRepository.GetUserRolesAsync(user.Username);
            return user;
        }

        public async Task<SignUpModel> CreateUserAsync(SignUpModel signUpModel)
        {
            if (await _userRepository.FindByNameAsync(signUpModel.Username) != null)
            {
                signUpModel.Errors.Add(Constants.ServiceValidationErrors.UserExistsError);
                return signUpModel;
            }

            //Create user 
            var repoResult = await _userRepository.CreateAsync(_mapper.Map<Users>(signUpModel), signUpModel.Password);
            if (!repoResult)
            {
                signUpModel.Errors.Add(Constants.ServiceValidationErrors.CreateUserError);
                return signUpModel;
            }

            //Find user
            var user = await _userRepository.FindByNameAsync(signUpModel.Username);

            //Add user to role
            var result = await _userRepository.AddToRoleAsync(user.Id, "Client");
            if (!result)
            {
                signUpModel.Errors.Add(Constants.ServiceValidationErrors.RoleNotExistsError);
                return signUpModel;
            }

            //Unblock user
            result = await _userRepository.LockOutAsync(user.UserName, false);
            if (!result)
            {
                signUpModel.Errors.Add(Constants.ServiceValidationErrors.UnlockUserError);
            }

            return signUpModel;
        }

        public async Task<UserModelItem> UpdateUserAsync(SignUpModel signUpModel)
        {
            var user = await _userRepository.FindByNameAsync(signUpModel.Username);
            var userModel = new UserModelItem();
            if(user == null)
            {
                userModel.Errors.Add(Constants.ServiceValidationErrors.UserNotExistsError);
                return userModel;
            }

            user.FirstName = signUpModel.Firstname;
            user.LastName = signUpModel.Lastname;

            var result = await _userRepository.UpdateAsync(user);
            if (!result)
            {
                userModel.Errors.Add(Constants.ServiceValidationErrors.EditUserError);
            }

            return userModel;
        }

        public async Task<UserModelItem> DeleteUserAsync(string username)
        {
            var user = await _userRepository.FindByNameAsync(username);
            var userModel = new UserModelItem();
            if (user == null)
            {
                userModel.Errors.Add(Constants.ServiceValidationErrors.UserNotExistsError);
                return userModel;
            }

            var result = await _userRepository.RemoveAsync(user);
            if (!result)
            {
                userModel.Errors.Add(Constants.ServiceValidationErrors.DeleteUserError);
            }

            return userModel;
        }

        public async Task<UserModelItem> BlockUserAsync(string username, bool enabled)
        {
            var result = await _userRepository.LockOutAsync(username, enabled);
            var userModel = new UserModelItem();
            if(!result)
            {
                userModel.Errors.Add(Constants.ServiceValidationErrors.UnlockUserError);
            }

            return userModel;
        }

        public async Task<RoleModelItem> CreateRoleAsync(RoleModelItem roleModel)
        {
            var result = await _userRepository.CreateRoleAsync(roleModel.Rolename);
            if (!result)
            {
                roleModel.Errors.Add(Constants.ServiceValidationErrors.CreateRoleError);
            }

            return roleModel;
        }

        public async Task<RoleModelItem> RemoveRoleAsync(RoleModelItem roleModel)
        {
            var result = await _userRepository.DeleteRoleAsync(roleModel.Rolename);
            if (!result)
            {
                roleModel.Errors.Add(Constants.ServiceValidationErrors.DeleteRoleError);
            }

            return roleModel;
        }

        public async Task<UserRoleModelItem> AddUserToRoleAsync(UserRoleModelItem userRoleModel)
        {
            var user = await _userRepository.FindByNameAsync(userRoleModel.Username);
            if (user == null)
            {
                userRoleModel.Errors.Add(Constants.ServiceValidationErrors.UserNotExistsError);
                return userRoleModel;
            }

            var result = await _userRepository.AddToRoleAsync(user.Id, userRoleModel.Rolename);
            if (!result)
            {
                userRoleModel.Errors.Add(Constants.ServiceValidationErrors.RoleNotExistsError);
            }

            return userRoleModel;
        }

        public async Task<UserRoleModelItem> RemoveUserFromRoleAsync(UserRoleModelItem userRoleModel)
        {
            var user = await _userRepository.FindByNameAsync(userRoleModel.Username);
            if (user == null)
            {
                userRoleModel.Errors.Add(Constants.ServiceValidationErrors.UserNotExistsError);
                return userRoleModel;
            }

            var result = await _userRepository.RemoveFromRoleAsync(user.Id, userRoleModel.Rolename);
            if (!result)
            {
                userRoleModel.Errors.Add(Constants.ServiceValidationErrors.RoleNotExistsError);
            }

            return userRoleModel;
        }
    }
}
