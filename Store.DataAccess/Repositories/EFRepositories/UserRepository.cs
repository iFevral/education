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

        public async void Create(Users user)
        {
            await _userManager.CreateAsync(user);
            await _db.SaveChangesAsync();
        }
        public async void Update(Users user)
        {
            await _userManager.UpdateAsync(user);
            await _db.SaveChangesAsync();
        }

        public async void Remove(Users user)
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

        public async void CreateRole(string name)
        {
            //await _roleManager.CreateAsync(IdentityRole(name));

            await _db.SaveChangesAsync();
        }

        public async void DeleteRole(string name)
        {
            await _roleManager.DeleteAsync(await _roleManager.FindByNameAsync(name));
            await _db.SaveChangesAsync();
        }

        public async Task<bool> IsInRole(string id, string role)
        {
            return await _userManager.IsInRoleAsync(await FindById(id), role);
        }

        public async void AddToRole(string id, string role)
        {
            await _userManager.AddToRoleAsync(await FindById(id), role);
            await _db.SaveChangesAsync();
        }
        public async void RemoveFromRole(string id, string role)
        {
            await _userManager.RemoveFromRoleAsync(await FindById(id), role);
            await _db.SaveChangesAsync();
        }

        public async void SignIn(Users user, bool isPersistent)
        {
            await _signInManager.SignInAsync(user, isPersistent);
        }
    }
}
