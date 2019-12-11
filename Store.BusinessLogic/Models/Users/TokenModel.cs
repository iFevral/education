using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Users
{
    public class TokenModel : BaseModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
