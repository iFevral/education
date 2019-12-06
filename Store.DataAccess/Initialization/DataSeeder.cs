using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.DataAccess.Entities;
using Store.DataAccess.Entities.Enums;

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
        }

        public async Task Seed()
        {
            string clientrole = Enums.Role.RoleNames.Admin.ToString();
            var role = await _roleManager.FindByNameAsync(clientrole);
            if (role == null)
            {
                var newRole = new Role();
                newRole.Name = clientrole;

                await _roleManager.CreateAsync(newRole);
            }

            clientrole = Enums.Role.RoleNames.Client.ToString();
            role = await _roleManager.FindByNameAsync(clientrole);
            if (role == null)
            {
                var newRole = new Role();
                newRole.Name = clientrole;

                await _roleManager.CreateAsync(newRole);
            }

            var user = await _userManager.FindByNameAsync("Admin");
            if (user == null)
            {
                var newUser = new User();

                newUser.UserName = "Admin";
                newUser.LastName = "Admin";
                newUser.Email = "admin@example.com";
                newUser.EmailConfirmed = true;
                newUser.LockoutEnabled = false;

                await _userManager.CreateAsync(newUser, "4f1df324bfb6");
                await _userManager.SetLockoutEnabledAsync(newUser,false);
                await _userManager.AddToRoleAsync(newUser, "Admin");
            };

        }
    }
}
