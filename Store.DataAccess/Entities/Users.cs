using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess
{
    public partial class Users : IdentityUser<int>
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
        public int Id { get; set; }
        [StringLength(256)]
        public string UserName { get; set; }
        [StringLength(256)]
        public string NormalizedUserName { get; set; }
        [StringLength(256)]
        public string Email { get; set; }
        [StringLength(256)]
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

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
