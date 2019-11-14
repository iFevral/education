using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Models.Users;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.Repositories.EFRepository;
using Store.BusinessLogic.Services.Interfaces;
using AutoMapper;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private UserModel _users;
        private IMapper _mapper;
        private IUserRepository _userRepository;
        public UserService(ApplicationContext db,
                              UserManager<Users> userManager,
                              RoleManager<Roles> roleManager,
                              SignInManager<Users> signInManager,
                              IMapper mapper)
        {
            _users = new UserModel();
            _userRepository = new UserRepository(db, userManager, roleManager, signInManager);
            _mapper = mapper;
        }

        public async Task SignUp(SignUpModelItem signUpData)
        {
            await _userRepository.Create(_mapper.Map<Users>(signUpData), signUpData.Password);
        }

        public UserModel GetAllUsers()
        {
            var users = _userRepository.GetAll();

            foreach (var user in users)
            {
                _users.Items.Add(_mapper.Map<UserModelItem>(user));
            }

            return _users;
        }

        public async Task<UserModelItem> SignIn(SignInModelItem loginData)
        {
            UserModelItem userItem = null;
            if (await _userRepository.IsCreated(loginData.Username, loginData.Password))
            {
                var userData = await _userRepository.FindByName(loginData.Username);
                if (userData != null)
                {
                    userItem = _mapper.Map<UserModelItem>(userData);
                    userItem.Roles = await _userRepository.GetUserRoles(userItem.Id);
                }
            }
            return userItem;
        }
    }
}
/*TODO: SignIn, 
        SignUp, 
        SignOut, 
        ForgotPassword
*/
