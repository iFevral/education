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
        private UserManager<Users> _userManager;
        private SignInManager<Users> _signInManager;
        private IUserRepository _userRepository;
        public AccountService(ApplicationContext db,
                              UserManager<Users> userManager,
                              SignInManager<Users> signInManager,
                              IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _userRepository = new UserRepository(db);
        }


        public async Task<UserModel> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userModel = new UserModel();
            
            if (user == null)
            {
                userModel.Errors.Add("User is not found");
                return userModel;
            }
            var userItem = _mapper.Map<UserModelItem>(user);
            userItem.Roles = await _userManager.GetRolesAsync(user);

            if (userItem.Roles == null)
            {
                userModel.Errors.Add("User is not in any role");
                return userModel;
            }

            userModel.Users.Add(userItem);
            return userModel;
        }


        public async Task<UserModel> GetUserByName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var userModel = new UserModel();

            if (user == null)
            {
                userModel.Errors.Add("User is not found");
                return userModel;
            }
            var userItem = _mapper.Map<UserModelItem>(user);
            userItem.Roles = await _userManager.GetRolesAsync(user);

            if (userItem.Roles == null)
            {
                userModel.Errors.Add("User is not in any role");
                return userModel;
            }

            userModel.Users.Add(userItem);
            return userModel;
        }


        public async Task<UserModel> SignIn(SignInData loginData)
        {
            var userModel = new UserModel();
            //If user created it will get user info or will return empty UserModelItem
            var result = await _signInManager.PasswordSignInAsync(loginData.Username, loginData.Password, false, false);
            if(!result.Succeeded)
            {
                userModel.Errors.Add("Username or password is incorrect. Please check data.");
                return userModel;
            }

            userModel = await GetUserByName(loginData.Username);
            return userModel;
        }

        public async Task<UserModel> SignUp(SignUpData signUpData)
        {
            var userModel = new UserModel();
            
            //Check if user exists
            if(await _userManager.FindByNameAsync(signUpData.Username) != null)
            {
                userModel.Errors.Add("Email is already registered");
                return userModel;
            }

            //Create user 
            var result = await _userManager.CreateAsync(_mapper.Map<Users>(signUpData), signUpData.Password);
            if(!result.Succeeded)
            {
                userModel.Errors.Add("Creating user error");
                return userModel;
            }

            //Find user
            var user = await _userManager.FindByNameAsync(signUpData.Username);

            //Add user to role
            result = await _userManager.AddToRoleAsync(user, "Client");
            if(!result.Succeeded)
            {
                userModel.Errors.Add($"Role 'Client' doesn`t exists");
                return userModel;
            }
            //Unblock
            result = await _userManager.SetLockoutEnabledAsync(user, false);
            if (!result.Succeeded)
            {
                userModel.Errors.Add($"Unlock has failed");
                return userModel;
            }

            //Generate token for registration from repository
            userModel.EmailData.Token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            userModel.EmailData.Email = signUpData.Email;

            return userModel;
        }

        public async Task SignOut(string username, string ipfingerprint)
        {
            var user = await _userManager.FindByNameAsync(username);
            await _userRepository.RemoveRefreshToken(user, ipfingerprint);
        }

        public async Task<UserModel> ConfirmEmail(string username, string token)
        {
            var userModel = new UserModel();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                userModel.Errors.Add($"User not found");
                return userModel;
            }

            //Check received token for email confirmation
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
                userModel.Errors.Add($"Email confirmation error");
           
            return userModel;
        }

        public async Task<bool> IsAccountLocked(string username)
        {
            return await _userManager.GetLockoutEnabledAsync(await _userManager.FindByNameAsync(username));
        }

        public async Task<UserModel> ResetPassword(string email)
        {
            //Map from Users to UserModelItem
            var user = await _userManager.FindByEmailAsync(email);
            var userModel = new UserModel();
            
            if(user == null)
            {
                userModel.Errors.Add("User is not found");
                return userModel;
            }

            userModel.ResetPasswordData.Token = await _userManager.GeneratePasswordResetTokenAsync(user);
            userModel.ResetPasswordData.Email = email;

            return userModel;
        }


        public async Task ConfirmNewPassword(string email, string token, string newPassword)
        {
            //Map from Users to UserModelItem
            var user = await _userManager.FindByEmailAsync(email);
            
            //Check received token for new password confirmation
            await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task<bool> CheckAndRemoveRefreshToken(string username, string ipfingerprint, string token)
        {
            var user = await _userManager.FindByNameAsync(username);
            var correctToken = _userRepository.GetRefreshToken(user, ipfingerprint);
            return token.Equals(correctToken);
        }

        public async Task SaveRefreshToken(string username, string ipfingerprint, string newToken)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (_userRepository.GetRefreshToken(user, ipfingerprint) != null)
                await _userRepository.RemoveRefreshToken(user, ipfingerprint);

            await _userRepository.SaveRefreshToken(user, ipfingerprint, newToken);
        }
    }
}
