using Store.BusinessLogic.Models.Users;

namespace Store.Presentation.Helpers.Interface
{
    public interface IJwtHelper
    {
        public string GenerateToken(UserModelItem userModel, double expirationTime, string secretKey, bool isAccess);
        public long GetUserIdFromToken(string token);
    }
}
