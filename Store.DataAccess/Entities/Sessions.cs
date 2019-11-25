using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Store.DataAccess.Entities
{
    public class Sessions
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string UserId { get; set; }
        public string IPFingerprint { get; set; }
        public string RefreshToken { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Users.Sessions))]
        public virtual Users User { get; set; }
    }
}
