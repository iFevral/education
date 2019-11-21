using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    public partial class AuthorInBooks
    {
        [Key]
        public int AuthorId { get; set; }
        [Key]
        public int PrintingEditionId { get; set; }


        [ForeignKey(nameof(AuthorId))]
        [InverseProperty(nameof(Authors.AuthorInBooks))]
        public virtual Authors Author { get; set; }
        [ForeignKey(nameof(PrintingEditionId))]
        [InverseProperty(nameof(PrintingEditions.AuthorInBooks))]
        public virtual PrintingEditions PrintingEdition { get; set; }
    }
}
