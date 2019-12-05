using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.Models;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class UserRepository : IUserRepository
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IEnumerable<User>> GetAllAsync(FilterModel<User> filterModel)
        {
            var items = await _userManager.Users.Where(filterModel.Predicate).ToListAsync();

            items = filterModel.SortWay == 1
                ? items.OrderByDescending(x => x.GetType().GetProperty(filterModel.SortProperty).GetValue(x, null)).ToList()
                : items = items.OrderBy(x => x.GetType().GetProperty(filterModel.SortProperty).GetValue(x, null)).ToList();
        
            if(filterModel.Quantity > 0)
            {
                items = items.Skip(filterModel.StartIndex).Take(filterModel.Quantity).ToList();
            }

            return items;
        }

        public async Task<bool> CreateAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> RemoveAsync(User user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> LockOutAsync(string email, bool enabled)
        {
            var result = await _userManager.SetLockoutEnabledAsync(await FindByEmailAsync(email), enabled);
            return result.Succeeded;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string email)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(await FindByEmailAsync(email));
        }

        public async Task<bool> ConfirmEmailAsync(string email, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(await FindByEmailAsync(email), token);
            return result.Succeeded;
        }

        public async Task<bool> CheckEmailConfirmationAsync(string email)
        {
            return await _userManager.IsEmailConfirmedAsync(await FindByEmailAsync(email));
        }

        public async Task<bool> CheckSignInAsync(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            return result.Succeeded;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(await FindByEmailAsync(email));
        }

        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(await FindByEmailAsync(email), token, newPassword);
            return result.Succeeded;
        }

        public async Task<User> FindByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IList<string>> GetUserRolesAsync(string email)
        {
            return await _userManager.GetRolesAsync(await FindByEmailAsync(email));
        }

        public async Task<bool> AddToRoleAsync(string id, string rolename)
        {
            var result = await _userManager.AddToRoleAsync(await FindByIdAsync(id), rolename);
            return result.Succeeded;
        }
    }
}
