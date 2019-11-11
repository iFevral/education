using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataAccess.Entities
{
    [Table("AuthorInBooks", Schema = "dbo")]
    public class AuthorInBooks
    {
        [Key]
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int PrintingEditionId { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }


        public Authors Author { get;set; }
        public PrintingEditions PrintingEdition { get; set; }

    }
}
