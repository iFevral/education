using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Users
{
    public class SignUpModelItem : UserModelItem
    {
        [Required]
        public string Password { get; set; }
    }
}
