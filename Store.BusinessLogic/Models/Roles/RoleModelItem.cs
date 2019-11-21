using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Roles
{
    public class RoleModelItem
    {
        [Required]
        public string Role { get; set; }
    }
}
