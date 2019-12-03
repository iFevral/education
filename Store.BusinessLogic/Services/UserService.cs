using System.Threading.Tasks;
using Store.DataAccess.Entities;
using Store.BusinessLogic.Common;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Models.Roles;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Common.Mappers.Interface;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper<Users, UserModelItem> _mapper;
        private readonly IMapper<Users, SignUpModel> _signUpMapper;
        private readonly IUserRepository _userRepository;
        
        public UserService(IMapper<Users, UserModelItem> mapper,
                           IMapper<Users, SignUpModel> signUpMapper,
                           IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _signUpMapper = signUpMapper;
        }

        public async Task<UserModel> GetAllUsersAsync(UserFilter userFilter)
        {
            UserModel userModel = new UserModel();

            var usersFromRepo = userFilter.Quantity > 0
                ? await _userRepository.GetAsync(userFilter.Predicate,
                                                 userFilter.SortProperty,
                                                 userFilter.SortWay,
                                                 userFilter.StartIndex,
                                                 userFilter.Quantity)
                : await _userRepository.GetAllAsync(userFilter.Predicate,
                                                    userFilter.SortProperty,
                                                    userFilter.SortWay);

            if (usersFromRepo == null)
            {
                userModel.Errors.Add(Constants.Errors.UsersNotExistError);
                return userModel;
            }


            foreach (var user in usersFromRepo)
            {
                //Map from Users to UserModelItem
                userModel.Users.Add(_mapper.Map(user, new UserModelItem()));
            }

            return userModel;
        }

        public async Task<UserModelItem> GetUserByIdAsync(string id)
        {
            var user = _mapper.Map(await _userRepository.FindByIdAsync(id), new UserModelItem());
            if (user == null)
            {
                user = new UserModelItem();
                user.Errors.Add(Constants.Errors.UsersNotExistError);
                return user;
            }

            user.Roles = await _userRepository.GetUserRolesAsync(user.Username);
            return user;
        }

        public async Task<UserModelItem> GetUserByNameAsync(string username)
        {
            var user = _mapper.Map(await _userRepository.FindByNameAsync(username), new UserModelItem());
            if (user == null)
            {
                user = new UserModelItem();
                user.Errors.Add(Constants.Errors.UserNotExistsError);
                return user;
            }

            user.Roles = await _userRepository.GetUserRolesAsync(user.Username);
            return user;
        }

        public async Task<SignUpModel> CreateUserAsync(SignUpModel signUpModel)
        {
            if (await _userRepository.FindByEmailAsync(signUpModel.Email) != null)
            {
                signUpModel.Errors.Add(Constants.Errors.UserExistsError);
                return signUpModel;
            }

            //Create user 
            var repoResult = await _userRepository.CreateAsync(_signUpMapper.Map(signUpModel, new Users()), signUpModel.Password);
            if (!repoResult)
            {
                signUpModel.Errors.Add(Constants.Errors.CreateUserError);
                return signUpModel;
            }

            //Find user
            var user = await _userRepository.FindByEmailAsync(signUpModel.Email);

            //Add user to role
            var result = await _userRepository.AddToRoleAsync(user.Id, "Client");
            if (!result)
            {
                signUpModel.Errors.Add(Constants.Errors.RoleNotExistsError);
                return signUpModel;
            }

            //Unblock user
            result = await _userRepository.LockOutAsync(user.UserName, false);
            if (!result)
            {
                signUpModel.Errors.Add(Constants.Errors.UnlockUserError);
            }

            return signUpModel;
        }

        public async Task<UserModelItem> UpdateUserAsync(SignUpModel signUpModel)
        {
            var user = await _userRepository.FindByEmailAsync(signUpModel.Email);
            var userModel = new UserModelItem();
            if(user == null)
            {
                userModel.Errors.Add(Constants.Errors.UserNotExistsError);
                return userModel;
            }

            user.FirstName = signUpModel.Firstname;
            user.LastName = signUpModel.Lastname;

            var result = await _userRepository.UpdateAsync(user);
            if (!result)
            {
                userModel.Errors.Add(Constants.Errors.EditUserError);
            }

            return userModel;
        }

        public async Task<UserModelItem> DeleteUserAsync(string username)
        {
            var user = await _userRepository.FindByNameAsync(username);
            var userModel = new UserModelItem();
            if (user == null)
            {
                userModel.Errors.Add(Constants.Errors.UserNotExistsError);
                return userModel;
            }

            var result = await _userRepository.RemoveAsync(user);
            if (!result)
            {
                userModel.Errors.Add(Constants.Errors.DeleteUserError);
            }

            return userModel;
        }

        public async Task<UserModelItem> BlockUserAsync(string username, bool enabled)
        {
            var result = await _userRepository.LockOutAsync(username, enabled);
            var userModel = new UserModelItem();
            if(!result)
            {
                userModel.Errors.Add(Constants.Errors.UnlockUserError);
            }

            return userModel;
        }

        public async Task<RoleModelItem> CreateRoleAsync(RoleModelItem roleModel)
        {
            var result = await _userRepository.CreateRoleAsync(roleModel.Rolename);
            if (!result)
            {
                roleModel.Errors.Add(Constants.Errors.CreateRoleError);
            }

            return roleModel;
        }

        public async Task<RoleModelItem> RemoveRoleAsync(RoleModelItem roleModel)
        {
            var result = await _userRepository.DeleteRoleAsync(roleModel.Rolename);
            if (!result)
            {
                roleModel.Errors.Add(Constants.Errors.DeleteRoleError);
            }

            return roleModel;
        }

        public async Task<UserRoleModelItem> AddUserToRoleAsync(UserRoleModelItem userRoleModel)
        {
            var user = await _userRepository.FindByNameAsync(userRoleModel.Username);
            if (user == null)
            {
                userRoleModel.Errors.Add(Constants.Errors.UserNotExistsError);
                return userRoleModel;
            }

            var result = await _userRepository.AddToRoleAsync(user.Id, userRoleModel.Rolename);
            if (!result)
            {
                userRoleModel.Errors.Add(Constants.Errors.RoleNotExistsError);
            }

            return userRoleModel;
        }

        public async Task<UserRoleModelItem> RemoveUserFromRoleAsync(UserRoleModelItem userRoleModel)
        {
            var user = await _userRepository.FindByNameAsync(userRoleModel.Username);
            if (user == null)
            {
                userRoleModel.Errors.Add(Constants.Errors.UserNotExistsError);
                return userRoleModel;
            }

            var result = await _userRepository.RemoveFromRoleAsync(user.Id, userRoleModel.Rolename);
            if (!result)
            {
                userRoleModel.Errors.Add(Constants.Errors.RoleNotExistsError);
            }

            return userRoleModel;
        }
    }
}
