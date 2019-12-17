using System.Collections.Generic;
using Store.BusinessLogic.Models.Base;
using Store.DataAccess.Entities.Enums;

namespace Store.BusinessLogic.Models.Filters
{
    public class PrintingEditionFilterModel : BaseFilterModel
    {
        public string SearchQuery { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public IList<int> Types { get; set; }
        public Enums.PrintingEditions.Currency Currency { get; set; } = Enums.PrintingEditions.Currency.USD;
    }
}
