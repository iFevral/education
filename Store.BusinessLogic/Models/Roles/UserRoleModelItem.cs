using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Roles
{
    public class UserRoleModelItem : RoleModelItem
    {
        [Required]
        public string Username { get; set; }
    }
}
