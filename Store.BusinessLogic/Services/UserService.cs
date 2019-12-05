using System.Threading.Tasks;
using Store.BusinessLogic.Common;
using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Models.Filters;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Common.Mappers.User;
using Store.BusinessLogic.Common.Mappers.Filter;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> GetAllUsersAsync(UserFilterModel userFilter)
        {
            UserModel userModel = new UserModel();

            var usersFromRepo = await _userRepository.GetAllAsync(userFilter.MapToDataAccessModel());

            if (usersFromRepo == null)
            {
                userModel.Errors.Add(Constants.Errors.UsersNotExistError);
                return userModel;
            }


            foreach (var user in usersFromRepo)
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

            userModel.Roles = await _userRepository.GetUserRolesAsync(userModel.Email);
            return userModel;
        }

        public async Task<BaseModel> UpdateUserAsync(SignUpModel signUpModel)
        {
            var user = await _userRepository.FindByEmailAsync(signUpModel.Email);
            var userModel = new UserModelItem();
            if(user == null)
            {
                userModel.Errors.Add(Constants.Errors.UserNotExistsError);
                return userModel;
            }

            user.FirstName = signUpModel.FirstName;
            user.LastName = signUpModel.LastName;

            var result = await _userRepository.UpdateAsync(user);
            if (!result)
            {
                userModel.Errors.Add(Constants.Errors.EditUserError);
            }

            return userModel;
        }

        public async Task<BaseModel> DeleteUserAsync(string email) //todo use IsRemoved prop
        {
            var user = await _userRepository.FindByEmailAsync(email);
            var userModel = new UserModelItem();
            if (user == null)
            {
                userModel.Errors.Add(Constants.Errors.UserNotExistsError);
                return userModel;
            }
            user.isRemoved = true;
            var result = await _userRepository.UpdateAsync(user);
            if (!result)
            {
                userModel.Errors.Add(Constants.Errors.DeleteUserError);
            }

            return userModel;
        }

        public async Task<BaseModel> BlockUserAsync(string email, bool enabled) //todo update lockout
        {
            var result = await _userRepository.LockOutAsync(email, enabled);
            var userModel = new UserModelItem();
            if(!result)
            {
                userModel.Errors.Add(Constants.Errors.UnlockUserError);
            }

            return userModel;
        }
    }
}
