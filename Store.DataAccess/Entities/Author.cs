using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    [Table("Authors")]
    public class Author : BaseEntity
    {
        public string Name { get; set; }

        [Dapper.Contrib.Extensions.Computed]
        public virtual ICollection<AuthorInPrintingEdition> AuthorInPrintingEdition { get; set; }
    }
}
