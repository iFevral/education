﻿using System.Threading.Tasks;
using Store.BusinessLogic.Common;
using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using Store.BusinessLogic.Common.Mappers.User;
using Store.BusinessLogic.Common.Mappers.User.SignUp;
using Store.DataAccess.Entities.Enums;
using Store.DataAccess.Repositories.Interfaces;
using System.Linq;
using System;

namespace Store.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModelItem> GetUserByIdAsync(long id)
        {
            var user = await _userRepository.FindByIdAsync(id);

            var userModel = new UserModelItem();

            if (user == null)
            {
                userModel.Errors.Add(Constants.Errors.UserNotExistsError);
                return userModel;
            }

            if (user.LockoutEnabled)
            {
                userModel.Errors.Add(Constants.Errors.UserLockError);
                return userModel;
            }

            userModel = user.MapToModel();
            userModel.Roles = await _userRepository.GetUserRolesAsync(user.Id);

            if (userModel.Roles == null)
            {
                userModel.Errors.Add(Constants.Errors.UserNotInAnyRoleError);
            }

            return userModel;
        }

        public async Task<UserModelItem> SignInAsync(SignInModel signInModel)
        {
            var user = await _userRepository.FindByEmailAsync(signInModel.Email);

            var userModel = new UserModelItem();

            if(user == null)
            {
                userModel.Errors.Add(Constants.Errors.UserNotExistsError);
                return userModel;
            }

            if (!user.EmailConfirmed)
            {
                userModel.Errors.Add(Constants.Errors.EmailConfirmationError);
                return userModel;
            }


            if (user.LockoutEnabled)
            {
                userModel.Errors.Add(Constants.Errors.UserLockError);
                return userModel;
            }

            userModel = user.MapToModel();

            userModel.Roles = await _userRepository.GetUserRolesAsync(user.Id);
            
            var result = await _userRepository.CheckSignInAsync(signInModel.Email, signInModel.Password);
            if(!result)
            {
                userModel.Errors.Add(Constants.Errors.WrongCredentialsError);
            }

            return userModel;
        }

        public async Task<EmailConfirmationModel> SignUpAsync(SignUpModel signUpModel)
        {
            var resultModel = new EmailConfirmationModel();

            var user = await _userRepository.FindByEmailAsync(signUpModel.Email);

            if (user != null)
            {
                resultModel.Errors.Add(Constants.Errors.EmailExistsError);
                return resultModel;
            }


            var result = await _userRepository.CreateAsync(signUpModel.MapToEntity(), signUpModel.Password);
            if(!result)
            {
                resultModel.Errors.Add(Constants.Errors.CreateUserError);
                return resultModel;
            }

            user = await _userRepository.FindByEmailAsync(signUpModel.Email);
            if (user == null)
            {
                resultModel.Errors.Add(Constants.Errors.UserNotExistsError);
                return resultModel;
            }

            result = await _userRepository.AddToRoleAsync(user.Id, Enums.Role.RoleNames.Client.ToString()); 
            if(!result)
            {
                resultModel.Errors.Add(Constants.Errors.RoleNotExistsError);
                return resultModel;
            }

            resultModel.Email = signUpModel.Email;
            resultModel.Token = await _userRepository.GenerateEmailConfirmationTokenAsync(user.Email);

            return resultModel;
        }

        public async Task<BaseModel> ConfirmEmailAsync(string email, string token)
        {
            var emailModel = new EmailConfirmationModel();

            var user = await _userRepository.FindByEmailAsync(email);
            if (user == null)
            {
                emailModel.Errors.Add(Constants.Errors.UsersNotExistError);
                return emailModel;
            }

            var result = await _userRepository.ConfirmEmailAsync(email, token);
            if (!result)
            {
                emailModel.Errors.Add(Constants.Errors.EmailConfirmationError);
            }

            result = await _userRepository.UnlockAsync(user.Email);
            if (!result)
            {
                emailModel.Errors.Add(Constants.Errors.UserLockError);
                return emailModel;
            }

            return emailModel;
        }

        public async Task<ResetPasswordModel> ResetPasswordAsync(string email)
        {
            var user = await _userRepository.FindByEmailAsync(email);

            var resetPasswordModel = new ResetPasswordModel();
            resetPasswordModel.Email = email;

            if (user == null)
            {
                resetPasswordModel.Errors.Add(Constants.Errors.UserNotExistsError);
                return resetPasswordModel;
            }

            Random random = new Random();
            var newPassword = new string(Enumerable.Repeat(Constants.PasswordGeneratorSettings.chars,
                                                                       Constants.PasswordGeneratorSettings.size)
                                                               .Select(s => s[random.Next(s.Length)]).ToArray());

            var resetToken = await _userRepository.GeneratePasswordResetTokenAsync(email);
            
            resetPasswordModel.Password = newPassword;
            
            var result = await _userRepository.ResetPasswordAsync(email, resetToken, newPassword);
            if (!result)
            {
                resetPasswordModel.Errors.Add(Constants.Errors.UserNotExistsError);
            }

            return resetPasswordModel;
        }

        public async Task<BaseModel> UpdateProfile(SignUpModel signUpModel)
        {
            var user = await _userRepository.FindByIdAsync(signUpModel.Id);
            var userModel = new UserModelItem();
            if (user == null)
            {
                userModel.Errors.Add(Constants.Errors.UserNotExistsError);
                return userModel;
            }

            user.FirstName = signUpModel.FirstName;
            user.LastName = signUpModel.LastName;
            user.Email = signUpModel.Email;

            var result = await _userRepository.UpdateAsync(user, signUpModel.Password);
            if (!result)
            {
                userModel.Errors.Add(Constants.Errors.EditUserError);
            }

            return userModel;
        }
    }
}
