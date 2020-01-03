using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Extensions.Sorting;
using System;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IEnumerable<User> GetAll(FilterModel<User> filterModel, out int counter)
        {
            var items = _userManager.Users
                                    .Where(filterModel.Predicate)
                                    .AsEnumerable()
                                    .SortBy(filterModel.SortProperty.ToString(), filterModel.IsAscending);

            counter = items.Count();

            if (filterModel.Quantity > 0)
            {
                items = items.Skip(filterModel.StartIndex).Take(filterModel.Quantity);
            }

            return items.ToList();
        }

        public async Task<bool> CreateAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> UpdateAsync(User user, string currentPassword, string newPassword)
        {
            var result = await _userManager.UpdateAsync(user);
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                var result2 = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
                return result.Succeeded && result2.Succeeded;
            }

            return result.Succeeded;
        } 

        public async Task<bool> RemoveAsync(User user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> LockAsync(string email)
        {
            var user = await FindByEmailAsync(email);
            var result = await _userManager.SetLockoutEnabledAsync(user, true);
            result = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            return result.Succeeded;
        }

        public async Task<bool> UnlockAsync(string email)
        {
            var user = await FindByEmailAsync(email);
            var lockoutResult = await _userManager.SetLockoutEnabledAsync(user, false);
            var lockoutEndDateResult = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MinValue);
            return lockoutResult.Succeeded || lockoutEndDateResult.Succeeded;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string email)
        {
            var user = await FindByEmailAsync(email);
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return token;
        }

        public async Task<bool> ConfirmEmailAsync(string email, string token)
        {
            var user = await FindByEmailAsync(email);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }

        public async Task<bool> CheckEmailConfirmationAsync(string email)
        {
            var user = await FindByEmailAsync(email);
            return await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<bool> CheckSignInAsync(string email, string password)
        {
            var user = await FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            return result.Succeeded;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await FindByEmailAsync(email);
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(await FindByEmailAsync(email), token, newPassword);
            return result.Succeeded;
        }

        public async Task<User> FindByIdAsync(long id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return user;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<string> GetUserRolesAsync(long id)
        {
            var user = await FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            return roles.First();
        }

        public async Task<bool> AddToRoleAsync(long id, string rolename)
        {
            var user = await FindByIdAsync(id);
            var result = await _userManager.AddToRoleAsync(user, rolename);
            return result.Succeeded;
        }
    }
}
