using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess
{
    public partial class UserInRoles : IdentityUserRole<int>
    {
        [Key]
        public int UserId { get; set; }
        [Key]
        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(Roles.UserInRoles))]
        public virtual Roles Role { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Users.UserInRoles))]
        public virtual Users User { get; set; }
    }
}
