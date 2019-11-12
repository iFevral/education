using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess
{
    public partial class Roles : IdentityRole<int>
    {
        public Roles()
        {
            AspNetRoleClaims = new HashSet<AspNetRoleClaims>();
            UserInRoles = new HashSet<UserInRoles>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(256)]
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
        [StringLength(256)]
        public string Name { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<UserInRoles> UserInRoles { get; set; }
    }
}
