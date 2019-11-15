using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    public partial class AspNetUserLogins : IdentityUserLogin<string>
    {
        [Key]
        public override string LoginProvider { get; set; }
        [Key]
        public override string ProviderKey { get; set; }
        public override string ProviderDisplayName { get; set; }
        public override string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Users.AspNetUserLogins))]
        public virtual Users User { get; set; }
    }
}
