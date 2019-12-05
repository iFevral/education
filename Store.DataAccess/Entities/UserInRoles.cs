using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Store.DataAccess.Entities
{
    public partial class UserInRoles : IdentityUserRole<string>
    {
        [Key]
        public override string UserId { get; set; }
        [Key]
        public override string RoleId { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}