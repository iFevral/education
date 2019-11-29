using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.BusinessLogic.Common;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Repositories.EFRepository;

namespace Store.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public AccountService(UserManager<Users> userManager,
                              SignInManager<Users> signInManager,
                              RoleManager<Roles> roleManager,
                              IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = new UserRepository(userManager,roleManager,signInManager);
        }


        public async Task<UserModelItem> GetUserByIdAsync(string id)
        {
            var user = await _userRepository.FindByIdAsync(id);
            var userModel = new UserModelItem();
            if (user == null)
            {
                userModel.Errors.Add(Constants.ServiceValidationErrors.UserNotExistsError);
                return userModel;
            }
            userModel = _mapper.Map<UserModelItem>(user);
            userModel.Roles = await _userRepository.GetUserRolesAsync(user.UserName);

            if (userModel.Roles == null)
            {
                userModel.Errors.Add(Constants.ServiceValidationErrors.UserNotInAnyRoleError);
                return userModel;
            }

            return userModel;
        }

        public async Task<UserModelItem> GetUserByNameAsync(string username)
        {
            var user = await _userRepository.FindByNameAsync(username);
            var userModel = new UserModelItem();
            if (user == null)
            {
                userModel.Errors.Add(Constants.ServiceValidationErrors.UserNotExistsError);
                return userModel;
            }
            userModel = _mapper.Map<UserModelItem>(user);
            userModel.Roles = await _userRepository.GetUserRolesAsync(user.Id);

            if (userModel.Roles == null)
            {
                userModel.Errors.Add(Constants.ServiceValidationErrors.UserNotInAnyRoleError);
                return userModel;
            }

            return userModel;
        }

        public async Task<UserModelItem> SignInAsync(SignInModel signInModel)
        {
            var user = await _userRepository.FindByNameAsync(signInModel.Username);
            var userModel = new UserModelItem();
            if(user == null)
            {
                userModel.Errors.Add("User not found");
            }

            userModel = _mapper.Map<UserModelItem>(user);
            userModel.Roles = await _userRepository.GetUserRolesAsync(signInModel.Username);
            //If user created it will get user info or will return empty UserModelItem
            var result = await _userRepository.CheckSignInAsync(signInModel.Username, signInModel.Password);
            if(!result)
            {
                userModel.Errors.Add("Username or password is incorrect. Please check data.");
                return userModel;
            }

            return userModel;
        }

        public async Task<EmailModel> SignUpAsync(SignUpModel signUpModel)
        {
            var emailModel = new EmailModel();
            //Check if user exists
            if(await _userRepository.FindByNameAsync(signUpModel.Username) != null)
            {
                emailModel.Errors.Add("Email is already registered");
                return emailModel;
            }

            //Create user 
            var result = await _userRepository.CreateAsync(_mapper.Map<Users>(signUpModel), signUpModel.Password);
            if(!result)
            {
                emailModel.Errors.Add("Creating user error");
                return emailModel;
            }

            //Find user
            var user = await _userRepository.FindByNameAsync(signUpModel.Username);

            //Add user to role
            result = await _userRepository.AddToRoleAsync(user.Id, "Client");
            if(!result)
            {
                emailModel.Errors.Add($"Role 'Client' doesn`t exists");
                return emailModel;
            }

            //Unblock
            result = await _userRepository.LockOutAsync(user.UserName, false);
            if (!result)
            {
                emailModel.Errors.Add($"Unlock has failed");
                return emailModel;
            }

            //Generate token for registration from repository
            emailModel.Email = signUpModel.Email;
            emailModel.Token = await _userRepository.GenerateEmailConfirmationTokenAsync(user.UserName);

            return emailModel;
        }

        public async Task<EmailModel> ConfirmEmailAsync(string username, string token)
        {
            var emailModel = new EmailModel();
            var user = await _userRepository.FindByNameAsync(username);
            if (user == null)
            {
                emailModel.Errors.Add($"User not found");
                return emailModel;
            }

            //Check received token for email confirmation
            var result = await _userRepository.ConfirmEmailAsync(username, token);
            if (!result)
            {
                emailModel.Errors.Add($"Email confirmation error");
            }

            return emailModel;
        }

        public async Task<bool> IsAccountLockedAsync(string username)
        {
            return await _userRepository.IsLockedOutAsync(username);
        }

        public async Task<ResetPasswordModel> ResetPasswordAsync(string email)
        {
            //Map from Users to UserModelItem
            var user = await _userRepository.FindByEmailAsync(email);            
            var resetPasswordModel = new ResetPasswordModel();
            if(user == null)
            {
                resetPasswordModel.Errors.Add("User is not found");
                return resetPasswordModel;
            }

            resetPasswordModel.Email = email;
            resetPasswordModel.Token = await _userRepository.GeneratePasswordResetTokenAsync(user.UserName);

            return resetPasswordModel;
        }

        public async Task<ResetPasswordModel> ConfirmNewPasswordAsync(string email, string token, string newPassword)
        {
            //Map from Users to UserModelItem
            var user = await _userRepository.FindByEmailAsync(email);
            var resetPasswordModel = new ResetPasswordModel();
            if (user == null)
            {
                resetPasswordModel.Errors.Add("User is not found");
                return resetPasswordModel;
            }

            //Check received token for new password confirmation
            var result = await _userRepository.ConfirmNewPasswordAsync(user.UserName, token, newPassword);
            if (!result)
            {
                resetPasswordModel.Errors.Add("User is not found");
            }

            return resetPasswordModel;
        }
    }
}
