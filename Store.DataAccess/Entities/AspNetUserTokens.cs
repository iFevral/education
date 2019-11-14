using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    public partial class AspNetUserTokens : IdentityUserToken<string>
    {
        [Key]
        public override string UserId { get; set; }
        [Key]
        public override string LoginProvider { get; set; }
        [Key]
        public override string Name { get; set; }
        public override string Value { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Users.AspNetUserTokens))]
        public virtual Users User { get; set; }
    }
}
