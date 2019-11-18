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

        public async Task <UserModel> GetAllUsers()
        {
            var users = await _userRepository.GetAll();

            foreach (var user in users)
            {
                //Map from Users to UserModelItem
                _users.Items.Add(_mapper.Map<UserModelItem>(user));
            }

            return _users;
        }

        public async Task<UserModelItem> GetUser(string username)
        {
            var user = _mapper.Map<UserModelItem>(await _userRepository.FindByName(username));
            if (user != null)
            {
                user.Roles = await _userRepository.GetUserRoles(user.UserName);
                return user;
            }
            
            throw new System.Exception("User not found");
        }

        public async Task<UserModelItem> SignIn(SignInModelItem loginData)
        {
            UserModelItem user;

            //If user created it will get user info or will return empty UserModelItem
            if (await _userRepository.IsPasswordCorrect(loginData.Username, loginData.Password))
            {
               return await GetUser(loginData.Username);
            }
            throw new System.Exception("User not found");
        }
        public async Task<string> SignUp(SignUpModelItem signUpData)
        {
            //Create user from repository
            await _userRepository.Create(_mapper.Map<Users>(signUpData), signUpData.Password);
            
            var user = await _userRepository.FindByName(signUpData.UserName);
            await _userRepository.AddToRole(user.Id, "Client");
            //Generate token for registration from repository
            return await _userRepository.GenerateRegistrationToken(signUpData.UserName);
        }

        public async Task<UserModelItem> SignOut()
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> ConfirmEmail(string username, string token)
        {
            //Check received token for email confirmation
            return await _userRepository.ConfirmEmail(username, token);
        }

        public async Task<bool> IsEmailConfirmed(string username)
        {
            //Check received token for email confirmation
            return await _userRepository.IsEmailConfirmed(username);
        }

        public async Task<string> ResetPassword(string email)
        {
            var user = _mapper.Map<UserModelItem>(await _userRepository.FindByEmail(email));
            //Generate token for password reset from repository
            return await _userRepository.GeneratePasswordResetToken(user.UserName);
        }

        public async Task ConfirmNewPassword(string email, string token, string newPassword)
        {
            var user = _mapper.Map<UserModelItem>(await _userRepository.FindByEmail(email));
            //Check received token for new password confirmation
            await _userRepository.ConfirmNewPassword(user.UserName, token, newPassword);
        }
    }
}
