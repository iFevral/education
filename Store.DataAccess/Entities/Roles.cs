﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    public partial class Roles : IdentityRole<string>
    {
        public Roles()
        {
            AspNetRoleClaims = new HashSet<AspNetRoleClaims>();
            UserInRoles = new HashSet<UserInRoles>();
        }

        [Key]
        public override string Id { get; set; }
        [StringLength(50)]
        public override string NormalizedName { get; set; }
        public override string ConcurrencyStamp { get; set; }
        [StringLength(50)]
        public override string Name { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<UserInRoles> UserInRoles { get; set; }
    }
}
