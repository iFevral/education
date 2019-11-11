using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    [Table("Authors", Schema = "dbo")]
    public class Authors
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }


        public ICollection<AuthorInBooks> AuthorInBooks { get; set; }
    }
}
