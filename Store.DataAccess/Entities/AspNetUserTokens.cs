using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    public partial class AspNetUserTokens : IdentityUserToken<string>
    {
        [Key]
        public string UserId { get; set; }
        [Key]
        public string LoginProvider { get; set; }
        [Key]
        public string Name { get; set; }
        public string Value { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Users.AspNetUserTokens))]
        public virtual Users User { get; set; }
    }
}
