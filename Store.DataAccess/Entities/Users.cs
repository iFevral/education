using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    public partial class Users : IdentityUser<string>
    {
        public Users()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserTokens = new HashSet<AspNetUserTokens>();
            Orders = new HashSet<Orders>();
            UserInRoles = new HashSet<UserInRoles>();
        }

        [Key]
        public override string Id { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public override string UserName { get; set; }
        [StringLength(50)]
        public override string NormalizedUserName { get; set; }
        [StringLength(50)]
        public override string Email { get; set; }
        [StringLength(50)]
        public override string NormalizedEmail { get; set; }
        public override bool EmailConfirmed { get; set; } = false;
        public override string PasswordHash { get; set; }
        public override string SecurityStamp { get; set; }
        public override string ConcurrencyStamp { get; set; }
        [StringLength(20)]
        public override string PhoneNumber { get; set; }
        public override bool PhoneNumberConfirmed { get; set; } = false;
        public override bool TwoFactorEnabled { get; set; } = false;
        public override DateTimeOffset? LockoutEnd { get; set; }
        public override bool LockoutEnabled { get; set; }
        public override int AccessFailedCount { get; set; } = 0;


        [InverseProperty("User")]
        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Orders> Orders { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserInRoles> UserInRoles { get; set; }
    }
}
