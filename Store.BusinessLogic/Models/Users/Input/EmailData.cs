using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Users
{
    public class EmailData
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string Token { get; set; }
    }
}
