using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess
{
    public partial class UserInRoles : IdentityUserRole<string>
    {
        [Key]
        public override string UserId { get; set; }
        [Key]
        public override string RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(Roles.UserInRoles))]
        public virtual Roles Role { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Users.UserInRoles))]
        public virtual Users User { get; set; }
    }
}
