using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Users
{
    public class UserModelItem : BaseModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
