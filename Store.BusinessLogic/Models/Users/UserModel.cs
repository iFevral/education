using Store.BusinessLogic.Models.Base;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.Users
{
    public class UserModel : BaseModel
    {
        public EmailData EmailData = new EmailData();
        public AccessTokenData AccessTokenOutputData = new AccessTokenData();
        public ResetPasswordData ResetPasswordData = new ResetPasswordData();
        public IList<UserModelItem> Users = new List<UserModelItem>();
    }
}
