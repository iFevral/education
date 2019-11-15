using System.Threading.Tasks;
using Store.BusinessLogic.Models.Users;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        public UserModel GetAllUsers();
        public Task<UserModelItem> SignIn(SignInModelItem loginData);
        public Task<UserModelItem> SignUp(SignUpModelItem userData);
    }
}
