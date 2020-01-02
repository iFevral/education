using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    [Table("AuthorInPrintingEdition")]
    public class AuthorInPrintingEdition : BaseEntity
    {
        [Key]
        public long AuthorId { get; set; }
        [Key]
        public long PrintingEditionId { get; set; }


        [Dapper.Contrib.Extensions.Computed]
        public virtual Author Author { get; set; }
        
        [Dapper.Contrib.Extensions.Computed]
        public virtual PrintingEdition PrintingEdition { get; set; }
    }
}
