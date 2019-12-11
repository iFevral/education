using System.Threading.Tasks;
using Store.BusinessLogic.Common;
using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Common.Mappers.User;
using Store.DataAccess.Repositories.Interfaces;
using Store.BusinessLogic.Models.Filters;
using Store.BusinessLogic.Common.Mappers.Filter;
using Store.BusinessLogic.Common.Mappers.User.SignUp;

namespace Store.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> GetNumberOfUsers()
        {
            return await _userRepository.GetNumberOfUsers();
        }

        public async Task<UserModel> GetAllUsersAsync(UserFilterModel userFilterModel)
        {
            var userModel = new UserModel();
            var filterModel = userFilterModel.MapToEFFilterModel();
            var users = await _userRepository.GetAllAsync(filterModel);

            if (users == null)
            {
                userModel.Errors.Add(Constants.Errors.UsersNotExistError);
                return userModel;
            }

            foreach (var user in users)
            {
                userModel.Items.Add(user.MapToModel());
            }

            return userModel;
        }

        public async Task<UserModelItem> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.FindByEmailAsync(email);
            var userModel = new UserModelItem();
            if (user == null)
            {
                userModel.Errors.Add(Constants.Errors.UsersNotExistError);
                return userModel;
            }
            userModel = user.MapToModel();

            userModel.Roles = await _userRepository.GetUserRolesAsync(user.Id);
            return userModel;
        }

        public async Task<BaseModel> UpdateUserAsync(SignUpModel signUpModel)
        {
            var user = await _userRepository.FindByIdAsync(signUpModel.Id);
            var userModel = new UserModelItem();
            if(user == null)
            {
                userModel.Errors.Add(Constants.Errors.UserNotExistsError);
                return userModel;
            }

            user = signUpModel.MapToEntity(user);

            var result = await _userRepository.UpdateAsync(user, signUpModel.Password);
            if (!result)
            {
                userModel.Errors.Add(Constants.Errors.EditUserError);
            }

            return userModel;
        }

        public async Task<BaseModel> DeleteUserAsync(string email)
        {
            var user = await _userRepository.FindByEmailAsync(email);
            var userModel = new UserModelItem();
            if (user == null)
            {
                userModel.Errors.Add(Constants.Errors.UserNotExistsError);
                return userModel;
            }
            user.isRemoved = true;
            var result = await _userRepository.UpdateAsync(user, null);
            if (!result)
            {
                userModel.Errors.Add(Constants.Errors.DeleteUserError);
            }

            return userModel;
        }

        public async Task<BaseModel> SetLockingStatus(string email, bool enabled)
        {
            var result = enabled 
                ? await _userRepository.LockAsync(email)
                : await _userRepository.UnlockAsync(email);

            var userModel = new UserModelItem();
            if(!result)
            {
                userModel.Errors.Add(Constants.Errors.UnlockUserError);
            }

            return userModel;
        }
    }
}
