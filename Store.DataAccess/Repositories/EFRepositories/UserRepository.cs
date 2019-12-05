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

        public async Task<int> GetNumberOfUsers()
        {
            int counter = await _userManager.Users.Where(x => !x.isRemoved).CountAsync();
            return counter;
        }

        public async Task<IEnumerable<User>> GetAllAsync(FilterModel<User> filterModel)
        {
            var items = await _userManager.Users.Where(filterModel.Predicate).ToListAsync();

            items = (int)filterModel.SortWay == 1
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
            var user = await FindByEmailAsync(email);
            var result = await _userManager.SetLockoutEnabledAsync(user, enabled);
            return result.Succeeded;
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
            var result = await _signInManager.PasswordSignInAsync(user, password, false, true);
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

        public async Task<IList<string>> GetUserRolesAsync(string email)
        {
            var user = await FindByEmailAsync(email);
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> AddToRoleAsync(long id, string rolename)
        {
            var user = await FindByIdAsync(id);
            var result = await _userManager.AddToRoleAsync(user, rolename);
            return result.Succeeded;
        }
    }
}
