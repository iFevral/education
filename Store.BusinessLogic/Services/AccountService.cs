using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Models.Users;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.Repositories.EFRepository;
using Store.BusinessLogic.Services.Interfaces;

namespace Store.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private IMapper _mapper;
        private IUserRepository _userRepository;
        public AccountService(ApplicationContext db,
                              UserManager<Users> userManager,
                              RoleManager<Roles> roleManager,
                              SignInManager<Users> signInManager,
                              IMapper mapper)
        {
            _userRepository = new UserRepository(db, userManager, roleManager, signInManager);
            _mapper = mapper;
        }


        public async Task<UserModel> GetUserById(string id)
        {
            var user = _mapper.Map<UserModelItem>(await _userRepository.FindById(id));
            var userModel = new UserModel();
            
            if (user == null)
            {
                userModel.Errors.Add("User is not found");
                return userModel;
            }

            user.Roles = await _userRepository.GetUserRoles(user.Username);

            if (user.Roles == null)
            {
                userModel.Errors.Add("User is not in any role");
                return userModel;
            }

            userModel.Users.Add(user);
            return userModel;
        }


        public async Task<UserModel> GetUserByName(string username)
        {
            var user = _mapper.Map<UserModelItem>(await _userRepository.FindByName(username));
            var userModel = new UserModel();
            
            if (user == null)
            {
                userModel.Errors.Add("User is not found");
                return userModel;
            }

            user.Roles = await _userRepository.GetUserRoles(user.Username);

            if (user.Roles == null)
            {
                userModel.Errors.Add("User is not in any role");
                return userModel;
            }

            userModel.Users.Add(user);
            return userModel;
        }


        public async Task<UserModel> SignIn(SignInData loginData)
        {
            var userModel = new UserModel();
            //If user created it will get user info or will return empty UserModelItem
            if (!await _userRepository.IsLoginDataCorrect(loginData.Username, loginData.Password))
            {
                userModel.Errors.Add("Username or password is incorrect. Please check data.");
                return userModel;
            }

            return await GetUserByName(loginData.Username);
        }


        public async Task<UserModel> SignUp(SignUpData signUpData)
        {
            //Check if user exists
            var userModel = new UserModel(); 
            if(await _userRepository.FindByName(signUpData.Username) != null)
            {
                userModel.Errors.Add("Email is already registered");
                return userModel;
            }

            //Create user 
            await _userRepository.Create(_mapper.Map<Users>(signUpData), signUpData.Password);
            
            //Find user
            var user = await _userRepository.FindByName(signUpData.Username);

            //Add user to role
            await _userRepository.AddToRole(user.Id, "Client");

            //Unblock
            await _userRepository.LockOut(user.UserName, false);

            //Generate token for registration from repository
            userModel.EmailData.Token = await _userRepository.GenerateEmailConfirmationToken(signUpData.Username);
            userModel.EmailData.Email = signUpData.Email;

            return userModel;
        }

        public async Task<bool> ConfirmEmail(string username, string token)
        {
            //Check received token for email confirmation
            return await _userRepository.ConfirmEmail(username, token);
        }

        public async Task<bool> IsAccountLocked(string username)
        {
            return await _userRepository.IsLockedOut(username);
        }

        public async Task<UserModel> ResetPassword(string email)
        {
            //Map from Users to UserModelItem
            var user = _mapper.Map<UserModelItem>(await _userRepository.FindByEmail(email));

            var userModel = new UserModel();
            
            if(user == null)
            {
                userModel.Errors.Add("User is not found");
                return userModel;
            }

            userModel.ResetPasswordData.Token = await _userRepository.GeneratePasswordResetToken(user.Username);
            userModel.ResetPasswordData.Email = email;

            return userModel;
        }


        public async Task ConfirmNewPassword(string email, string token, string newPassword)
        {
            //Map from Users to UserModelItem
            var user = _mapper.Map<UserModelItem>(await _userRepository.FindByEmail(email));
            
            //Check received token for new password confirmation
            await _userRepository.ConfirmNewPassword(user.Username, token, newPassword);
        }

        public async Task<bool> CheckAndRemoveRefreshToken(string username, string ipfingerprint, string token)
        {
            var correctToken = await _userRepository.GetRefreshToken(username, ipfingerprint);
            return token.Equals(correctToken);
        }

        public async Task SaveRefreshToken(string username, string ipfingerprint, string newToken)
        {
            if(await _userRepository.GetRefreshToken(username, ipfingerprint) != null)
                await _userRepository.RemoveRefreshToken(username, ipfingerprint);

            await _userRepository.SaveRefreshToken(username, ipfingerprint, newToken);
        }
    }
}
