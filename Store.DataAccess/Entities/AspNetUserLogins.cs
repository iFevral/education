﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess
{
    public partial class AspNetUserLogins : IdentityUserLogin<int>
    {
        [Key]
        public string LoginProvider { get; set; }
        [Key]
        public string ProviderKey { get; set; }
        public string ProviderDisplayName { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Users.AspNetUserLogins))]
        public virtual Users User { get; set; }
    }
}
