using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess
{
    public partial class AuthorInBooks
    {
        [Key]
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int PrintingEditionId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [ForeignKey(nameof(AuthorId))]
        [InverseProperty(nameof(Authors.AuthorInBooks))]
        public virtual Authors Author { get; set; }
        [ForeignKey(nameof(PrintingEditionId))]
        [InverseProperty(nameof(PrintingEditions.AuthorInBooks))]
        public virtual PrintingEditions PrintingEdition { get; set; }
    }
}
