using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    public partial class Authors : BaseEntity
    {
        [StringLength(100)]
        public string Name { get; set; }

        [InverseProperty("Author")]
        public virtual ICollection<AuthorInBooks> AuthorInBooks { get; set; }
    }
}
