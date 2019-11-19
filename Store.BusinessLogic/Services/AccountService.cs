using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Store.BusinessLogic.Models.Users;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using Store.DataAccess.Repositories.EFRepository;
using Store.BusinessLogic.Services.Interfaces;

namespace Store.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private IMapper _mapper;
        private IUserRepository _userRepository;
        public AccountService(ApplicationContext db,
                              UserManager<Users> userManager,
                              RoleManager<Roles> roleManager,
                              SignInManager<Users> signInManager,
                              IMapper mapper)
        {
            _userRepository = new UserRepository(db, userManager, roleManager, signInManager);
            _mapper = mapper;
        }


        public async Task<UserModelItem> GetUserById(string id)
        {
            var user = _mapper.Map<UserModelItem>(await _userRepository.FindById(id));
            if (user != null)
            {
                user.Roles = await _userRepository.GetUserRoles(user.UserName);
                return user;
            }

            throw new System.Exception("User not found");
        }

        public async Task<UserModelItem> GetUserByName(string username)
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
            //If user created it will get user info or will return empty UserModelItem
            if (await _userRepository.IsPasswordCorrect(loginData.Username, loginData.Password))
                return await GetUserByName(loginData.Username);
            
            throw new System.Exception("User not found");
        }


        public async Task<string> SignUp(SignUpModelItem signUpData)
        {
            //Create user from repository
            await _userRepository.Create(_mapper.Map<Users>(signUpData), signUpData.Password);
            
            //Find user
            var user = await _userRepository.FindByName(signUpData.UserName);
            
            //Add user to role
            await _userRepository.AddToRole(user.Id, "Client");
            
            //Generate token for registration from repository
            return await _userRepository.GenerateEmailConfirmationToken(signUpData.UserName);
        }


        //TODO: Sign Out
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
            //Map from Users to UserModelItem
            var user = _mapper.Map<UserModelItem>(await _userRepository.FindByEmail(email));
            
            //Generate token for password reset from repository
            return await _userRepository.GeneratePasswordResetToken(user.UserName);
        }


        public async Task ConfirmNewPassword(string email, string token, string newPassword)
        {
            //Map from Users to UserModelItem
            var user = _mapper.Map<UserModelItem>(await _userRepository.FindByEmail(email));
            
            //Check received token for new password confirmation
            await _userRepository.ConfirmNewPassword(user.UserName, token, newPassword);
        }
    }
}
