using System.Threading.Tasks;
using Store.BusinessLogic.Common.Constants;
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

        public async Task<UserModel> GetAllUsersAsync(UserFilterModel userFilterModel)
        {
            var userModel = new UserModel();
            var filterModel = userFilterModel.MapToEFFilterModel();
            int counter = 0;
            var users = _userRepository.GetAll(filterModel, out counter);

            if (users == null)
            {
                userModel.Errors.Add(Constants.Errors.UsersNotExistError);
                return userModel;
            }

            userModel.Counter = counter;
            foreach (var user in users)
            {
                userModel.Items.Add(user.MapToModel());
            }

            return userModel;
        }

        public async Task<UserModelItem> GetUserAsync(long id)
        {
            var user = await _userRepository.FindByIdAsync(id);
            var userModel = new UserModelItem();
            if (user == null)
            {
                userModel.Errors.Add(Constants.Errors.UserNotExistsError);
                return userModel;
            }
            userModel = user.MapToModel();

            userModel.Role = await _userRepository.GetUserRolesAsync(user.Id);
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

            var result = await _userRepository.UpdateAsync(user, signUpModel.Password, signUpModel.NewPassword);
            if (!result)
            {
                userModel.Errors.Add(Constants.Errors.EditUserError);
            }

            return userModel;
        }

        public async Task<BaseModel> DeleteUserAsync(long id)
        {
            var user = await _userRepository.FindByIdAsync(id);
            var userModel = new UserModelItem();
            if (user == null)
            {
                userModel.Errors.Add(Constants.Errors.UserNotExistsError);
                return userModel;
            }
            user.isRemoved = true;
            var result = await _userRepository.UpdateAsync(user, null, null);
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
