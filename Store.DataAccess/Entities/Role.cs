using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    [Table("Roles")]
    public partial class Role : IdentityRole<long>
    {
        public virtual ICollection<UserInRoles> UserInRoles { get; set; }
    }
}
