using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Users
{
    public class ResetPasswordData : EmailData
    {
        [Required(ErrorMessage = "Token is required")]
        public string Token { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
