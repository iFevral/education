using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    public partial class AspNetRoleClaims : IdentityRoleClaim<string> 
    {
        [Key]
        public string Id { get; set; }
        public string RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(Roles.AspNetRoleClaims))]
        public virtual Roles Role { get; set; }
    }
}
