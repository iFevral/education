using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Users
{
    public class SignUpModel : BaseModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string Avatar { get; set; }
    }
}
