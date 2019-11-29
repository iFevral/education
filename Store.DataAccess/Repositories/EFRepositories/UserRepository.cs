using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class UserRepository : IUserRepository
    {
        private UserManager<Users> _userManager;
        private RoleManager<Roles> _roleManager;
        private SignInManager<Users> _signInManager;

        public UserRepository(UserManager<Users> userManager, RoleManager<Roles> roleManager, SignInManager<Users> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<bool> CreateAsync(Users user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> UpdateAsync(Users user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> RemoveAsync(Users user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> LockOutAsync(string username, bool enabled)
        {
            var result = await _userManager.SetLockoutEnabledAsync(await FindByNameAsync(username), enabled);
            return result.Succeeded;
        }

        public async Task<bool> IsLockedOutAsync(string username)
        {
            return await _userManager.GetLockoutEnabledAsync(await FindByNameAsync(username));
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string username)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(await FindByNameAsync(username));
        }

        public async Task<bool> ConfirmEmailAsync(string username, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(await FindByNameAsync(username), token);
            return result.Succeeded;
        }

        public async Task<bool> CheckEmailConfirmationAsync(string username)
        {
            return await _userManager.IsEmailConfirmedAsync(await FindByNameAsync(username));
        }

        public async Task<bool> CheckSignInAsync(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
            return result.Succeeded;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string username)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(await FindByNameAsync(username));
        }

        public async Task<bool> ConfirmNewPasswordAsync(string username, string token, string newPassword)
        {
            var result = await _userManager.ResetPasswordAsync(await FindByNameAsync(username), token, newPassword);
            return result.Succeeded;
        }

        public async Task<Users> FindByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<Users> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<Users> FindByNameAsync(string name)
        {
            return await _userManager.FindByNameAsync(name);
        }

        public async Task<IList<string>> GetUserRolesAsync(string username)
        {
            return await _userManager.GetRolesAsync(await FindByNameAsync(username));
        }

        public async Task<bool> CheckRoleAvailabilityAsync(string rolename)
        {
            return await _roleManager.RoleExistsAsync(rolename);
        }

        public async Task<bool> CreateRoleAsync(string rolename)
        {
            var result = await _roleManager.CreateAsync(new Roles { Name = rolename });
            return result.Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(string rolename)
        {
            var result = await _roleManager.DeleteAsync(await _roleManager.FindByNameAsync(rolename));
            return result.Succeeded;
        }

        public async Task<bool> CheckRoleAsync(string id, string rolename)
        {
            return await _userManager.IsInRoleAsync(await FindByIdAsync(id), rolename);
        }

        public async Task<bool> AddToRoleAsync(string id, string rolename)
        {
            var result = await _userManager.AddToRoleAsync(await FindByIdAsync(id), rolename);
            return result.Succeeded;
        }

        public async Task<bool> RemoveFromRoleAsync(string id, string rolename)
        {
            var result = await _userManager.RemoveFromRoleAsync(await FindByIdAsync(id), rolename);
            return result.Succeeded;
        }
    }
}
