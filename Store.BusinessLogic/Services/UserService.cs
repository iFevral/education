using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Models.Users;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.Repositories.EFRepository;
using Store.BusinessLogic.Services.Interfaces;

namespace Store.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private UserModel _users;
        private IUserRepository _userRepository;
        public UserService(ApplicationContext db,
                              UserManager<Users> userManager,
                              RoleManager<Roles> roleManager,
                              SignInManager<Users> signInManager)
        {
            _users = new UserModel();
            _userRepository = new UserRepository(db, userManager, roleManager, signInManager);
        }

        public UserModel GetAllUsers()
        {
            var users = _userRepository.GetAll();

            foreach (var user in users)
            {
                _users.Items.Add(new UserModelItem
                {
                    Id = user.Id,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName
                });
            }

            return _users;
        }
    }
}
/*TODO: SignIn, 
        SignUp, 
        SignOut, 
        ForgotPassword
*/
