using System;
using System.ComponentModel.DataAnnotations;

namespace Store.DataAccess.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime? CreationDate { get; set; } = DateTime.Now;
        public bool isRemoved { get; set; } = false;
    }
}
