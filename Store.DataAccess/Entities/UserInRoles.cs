using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Store.DataAccess.Entities
{
    [Table("UserInRoles", Schema = "dbo")]
    public class UserInRoles : IdentityUserRole
    {

    }
}
