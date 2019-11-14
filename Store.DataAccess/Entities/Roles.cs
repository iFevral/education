using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    public partial class Roles : IdentityRole<string>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set; }

        [StringLength(50)]
        public override string Name { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<UserInRoles> UserInRoles { get; set; }
    }
}
