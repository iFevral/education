using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Store.DataAccess.Entities
{
    [Table("Users")]
    public partial class User : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isRemoved { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
