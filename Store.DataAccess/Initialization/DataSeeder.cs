using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.DataAccess.Entities;
using Store.DataAccess.Entities.Enums;

namespace Store.DataAccess.Initialization
{
    public class DataSeeder
    {
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole<long>> _roleManager;
        public DataSeeder(UserManager<User> userManager, RoleManager<IdentityRole<long>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            string clientrole = Enums.Role.RoleName.Admin.ToString();
            var role = await _roleManager.FindByNameAsync(clientrole);
            if (role == null)
            {
                var newRole = new IdentityRole<long>();
                newRole.Name = clientrole;

                await _roleManager.CreateAsync(newRole);
            }

            clientrole = Enums.Role.RoleName.Client.ToString();
            role = await _roleManager.FindByNameAsync(clientrole);
            if (role == null)
            {
                var newRole = new IdentityRole<long>();
                newRole.Name = clientrole;

                await _roleManager.CreateAsync(newRole);
            }

            var user = await _userManager.FindByEmailAsync("admin@example.com");
            if (user == null)
            {
                var newUser = new User();

                newUser.UserName = "Admin";
                newUser.FirstName = "Admin";
                newUser.LastName = "Admin";
                newUser.Email = "admin@example.com";
                newUser.EmailConfirmed = true;

                await _userManager.CreateAsync(newUser, "4f1df324bfb6");
                await _userManager.SetLockoutEnabledAsync(newUser, false);
                await _userManager.SetLockoutEndDateAsync(newUser, DateTimeOffset.MinValue);
                await _userManager.AddToRoleAsync(newUser, "Admin");
            };
        }
    }
}
