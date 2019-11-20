using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Entities;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Interfaces;
using System.Linq;

namespace Store.DataAccess.Repositories.EFRepository
{
    public class UserRepository : IUserRepository
    {
        private ApplicationContext _db;
        private UserManager<Users> _userManager;
        private RoleManager<Roles> _roleManager;
        private SignInManager<Users> _signInManager;

        public UserRepository(ApplicationContext db, UserManager<Users> userManager, RoleManager<Roles> roleManager, SignInManager<Users> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task Create(Users user, string password)
        {
            await _userManager.CreateAsync(user, password);
            await _db.SaveChangesAsync();
        }
        
        public async Task Update(Users user)
        {
            await _userManager.UpdateAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task Remove(Users user)
        {
            await _userManager.DeleteAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task LockOut(string username,bool enabled)
        {
            await _userManager.SetLockoutEnabledAsync(await FindByName(username), enabled);
        }

        public async Task<bool> IsLockedOut(string username)
        {
            return await _userManager.GetLockoutEnabledAsync(await FindByName(username));
        }

        public async Task<string> GenerateEmailConfirmationToken(string username)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(await FindByName(username));
        }

        public async Task<bool> ConfirmEmail(string username, string token)
        {
            var b = token;
            var result = await _userManager.ConfirmEmailAsync(await FindByName(username), token);
            return result.Succeeded;
        }

        public async Task<bool> IsEmailConfirmed(string username)
        {
            return await _userManager.IsEmailConfirmedAsync(await FindByName(username));
        }

        public async Task<bool> IsLoginDataCorrect(string username, string password)
        {
            var a = await _signInManager.PasswordSignInAsync(username, password, false, false);
            return a.Succeeded;
        }

        public async Task<string> GeneratePasswordResetToken(string username)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(await FindByName(username));
        }

        public async Task ConfirmNewPassword(string username, string token,string newPassword)
        {
            await _userManager.ResetPasswordAsync(await FindByName(username), token, newPassword);
        }

        public async Task<Users> FindById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<Users> FindByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<Users> FindByName(string name)
        {
            return await _userManager.FindByNameAsync(name);
        }

        public async Task<IEnumerable<string>> GetUserRoles(string username)
        {
            return await _userManager.GetRolesAsync(await FindByName(username));
        }

        public async Task<bool> IsRoleCreated(string rolename)
        {
            return await _roleManager.RoleExistsAsync(rolename);
        }

        public async Task CreateRole(string rolename)
        {
            await _roleManager.CreateAsync(new Roles { Name = rolename });
            await _db.SaveChangesAsync();
        }
 
        public async Task DeleteRole(string rolename)
        {
            await _roleManager.DeleteAsync(await _roleManager.FindByNameAsync(rolename));
            await _db.SaveChangesAsync();
        }

        public async Task<bool> IsInRole(string id, string rolename)
        {
            return await _userManager.IsInRoleAsync(await FindById(id), rolename);
        }

        public async Task AddToRole(string id, string rolename)
        {
            await _userManager.AddToRoleAsync(await FindById(id), rolename);
            await _db.SaveChangesAsync();
        }

        public async Task RemoveFromRole(string id, string rolename)
        {
            await _userManager.RemoveFromRoleAsync(await FindById(id), rolename);
            await _db.SaveChangesAsync();
        }

        public async Task<string> GetRefreshToken(string username, string ipfingerprint)
        {
            var user = await FindByName(username);
            var session = _db.Sessions.Where(s => s.UserId.Equals(user.Id) && 
                                            s.IPFingerprint.Equals(ipfingerprint));
                
            return session.Any() 
                ? session.FirstOrDefault().RefreshToken 
                : null;
        }

        public async Task SaveRefreshToken(string username, string ipfingerprint, string newToken)
        {
            var user = await FindByName(username);
            _db.Sessions.Add(new Sessions 
            { 
                UserId = user.Id,
                IPFingerprint = ipfingerprint,
                RefreshToken = newToken
            });
            await _db.SaveChangesAsync();
        }

        public async Task RemoveRefreshToken(string username, string ipfingerprint)
        {
            var user = await FindByName(username);
            var session = _db.Sessions.Where(s => s.UserId.Equals(user.Id) &&
                                                  s.IPFingerprint.Equals(ipfingerprint)).FirstOrDefault();
            _db.Sessions.Remove(session);
            await _db.SaveChangesAsync();
        }
    }
}
