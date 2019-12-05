using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Store.DataAccess.Entities
{
    [Table("Users")]
    public partial class User : IdentityUser<string> //todo rename, id => long
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isRemoved { get; set; }


        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<UserInRoles> UserInRoles { get; set; }
    }
}
