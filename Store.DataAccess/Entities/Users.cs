using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    [Table("Users", Schema = "Public")]
    public class Users : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
