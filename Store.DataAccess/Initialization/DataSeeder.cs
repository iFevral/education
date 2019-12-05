using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.DataAccess.Entities;

namespace Store.DataAccess.Initialization
{
    public class DataSeeder
    {
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;
        public DataSeeder(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            Seed();
        }

        public async Task Seed()
        {
            if (await _roleManager.FindByNameAsync("Admin") != null)
            {
                var role = new Role
                {
                    Name = "Admin"
                };

                await _roleManager.CreateAsync(role);
            }

            if (await _userManager.FindByNameAsync("Admin") != null)
            {
                var user = new User
                {
                    UserName = "Admin",
                    Email = "admin@example.com",
                    EmailConfirmed = true,
                    LockoutEnabled = false
                };

                await _userManager.CreateAsync(user, "4f1df324bfb6");
                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
