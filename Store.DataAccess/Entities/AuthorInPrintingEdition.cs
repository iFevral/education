using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    [Table("AuthorInPrintingEdition")]
    public partial class AuthorInPrintingEdition : BaseEntity
    {
        [Key]
        public long AuthorId { get; set; }
        [Key]
        public long PrintingEditionId { get; set; }


        public virtual Author Author { get; set; }
        public virtual PrintingEdition PrintingEdition { get; set; }
    }
}
