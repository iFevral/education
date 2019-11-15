using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Store.BusinessLogic.Models.Users
{
    public class EmailModelItem
    {
        [Required]
        public string Email { get; set; }
    }
}
