using System.Collections.Generic;
using Store.BusinessLogic.Common.Constants;
using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Users
{
    public class UserModelItem : BaseModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsLocked { get; set; }
        public string Avatar { get; set; } = Constants.Images.DefaultUserAvatar;
        public IList<string> Roles { get; set; } = new List<string>();
    }
}