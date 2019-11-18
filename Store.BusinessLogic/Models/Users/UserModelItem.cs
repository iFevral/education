using Store.BusinessLogic.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Users
{
    public class UserModelItem : TokenModelItem
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
