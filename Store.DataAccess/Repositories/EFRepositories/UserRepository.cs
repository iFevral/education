using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;

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

        public IEnumerable<Users> GetAll()
        {
            return _userManager.Users.ToList();
        }

        public async Task Create(Users user,string password)
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

        public async Task<Users> FindById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<Users> FindByEmail(string email)
        {
            return await _userManager.FindByIdAsync(email);
        }

        public async Task<Users> FindByName(string name)
        {
            return await _userManager.FindByIdAsync(name);
        }

        public async Task<IEnumerable<string>> GetUserRoles(string id)
        {
            return await _userManager.GetRolesAsync(await FindById(id));
        }

        public async Task CreateRole(string name)
        {
            await _roleManager.CreateAsync(new Roles { Name = name});
            await _db.SaveChangesAsync();
        }

        public async Task DeleteRole(string name)
        {
            await _roleManager.DeleteAsync(await _roleManager.FindByNameAsync(name));
            await _db.SaveChangesAsync();
        }

        public async Task<bool> IsInRole(string id, string role)
        {
            return await _userManager.IsInRoleAsync(await FindById(id), role);
        }

        public async Task AddToRole(string id, string role)
        {
            await _userManager.AddToRoleAsync(await FindById(id), role);
            await _db.SaveChangesAsync();
        }
        public async Task RemoveFromRole(string id, string role)
        {
            await _userManager.RemoveFromRoleAsync(await FindById(id), role);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> IsCreated(string username, string password)
        {
            var a = await _signInManager.PasswordSignInAsync(username, password, false, false);

            return a.Succeeded;
        }
    }
}
