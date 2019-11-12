using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess
{
    public partial class AspNetRoleClaims : IdentityRoleClaim<int> 
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(Roles.AspNetRoleClaims))]
        public virtual Roles Role { get; set; }
    }
}
