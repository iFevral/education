using Store.BusinessLogic.Models.Base;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.Users
{
    public class UserModel : BaseModel
    {
        public ICollection<UserModelItem> Items = new List<UserModelItem>();
    }
}
