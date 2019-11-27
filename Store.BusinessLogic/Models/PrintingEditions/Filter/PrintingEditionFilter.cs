using System;
using System.Collections.Generic;
using System.Text;

namespace Store.BusinessLogic.Models.PrintingEditions
{
    public class PrintingEditionFilter
    {
        public string Title { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string Author { get; set; }
    }
}
