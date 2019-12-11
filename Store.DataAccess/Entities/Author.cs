using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    [Table("Authors")]
    public partial class Author : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<AuthorInPrintingEdition> AuthorInBooks { get; set; }
    }
}
