using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Users
{
    public class ForgotPasswordModelItem : EmailModelItem
    {
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
