using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.Orders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Users
{
    public class UserModelItem : AccessTokenData
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();

        public IList<OrderModelItem> Orders { get; set; } = new List<OrderModelItem>();
    }
}
