using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess
{
    public partial class Authors
    {
        public Authors()
        {
            AuthorInBooks = new HashSet<AuthorInBooks>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [InverseProperty("Author")]
        public virtual ICollection<AuthorInBooks> AuthorInBooks { get; set; }
    }
}
