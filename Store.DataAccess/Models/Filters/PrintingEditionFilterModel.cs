using System.Collections.Generic;
using Store.DataAccess.Entities;

namespace Store.DataAccess.Models.Filters
{
    public class PrintingEditionFilterModel : FilterModel<PrintingEdition>
    {
        public string SearchQuery { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public IList<int> Types { get; set; }
    }
}
