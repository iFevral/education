﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    public partial class AspNetUserClaims : IdentityUserClaim<string>
    {
        [Key]
        public string Id { get; set; }
        public override string UserId { get; set; }
        public override string ClaimType { get; set; }
        public override string ClaimValue { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Users.AspNetUserClaims))]
        public virtual Users User { get; set; }
    }
}
