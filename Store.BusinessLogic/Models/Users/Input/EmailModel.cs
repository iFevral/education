using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Users
{
    public class EmailModel : BaseModel
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string Token { get; set; }
    }
}
