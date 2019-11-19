using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    public partial class Users : IdentityUser<string>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public override string UserName { get; set; }
        [StringLength(50)]
        public override string Email { get; set; }
        public override bool EmailConfirmed { get; set; } = false;
        public override string PasswordHash { get; set; }


        [InverseProperty("User")]
        public virtual ICollection<Orders> Orders { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserInRoles> UserInRoles { get; set; }
    }
}
