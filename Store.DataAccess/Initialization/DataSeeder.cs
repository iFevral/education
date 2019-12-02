using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.DataAccess.Entities;

namespace Store.DataAccess.Initialization
{
    public class DataSeeder
    {
        private UserManager<Users> _userManager;
        private RoleManager<Roles> _roleManager;
        public DataSeeder(UserManager<Users> userManager, RoleManager<Roles> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            Seed();
        }

        public async Task Seed()
        {
            if (await _roleManager.FindByNameAsync("Admin") != null)
            {
                var role = new Roles
                {
                    Name = "Admin"
                };

                await _roleManager.CreateAsync(role);
            }

            if (await _userManager.FindByNameAsync("Admin") != null)
            {
                var user = new Users
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
