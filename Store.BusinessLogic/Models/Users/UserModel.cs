using Store.BusinessLogic.Models.Base;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.Users
{
    public class UserModel : BaseModel
    {
        public IList<UserModelItem> Items = new List<UserModelItem>();
    }
}
