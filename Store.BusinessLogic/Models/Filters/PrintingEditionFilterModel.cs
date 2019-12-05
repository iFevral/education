using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Filters
{
    public class PrintingEditionFilterModel : BaseFilterModel<DataAccess.Entities.PrintingEdition>
    {
        public string SearchQuery { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public IList<int> Types { get; set; }

        public override Expression<Func<DataAccess.Entities.PrintingEdition, bool>> Predicate
        {
            get
            {
                return printingEdition => (this.Types == null || this.Types.Count == 0 || this.Types.Any(t => t == (int)printingEdition.Type)) &&
                                          (this.MinPrice == null || printingEdition.Price >= this.MinPrice) &&
                                          (this.MaxPrice == null || printingEdition.Price <= this.MaxPrice) &&
                                          (!printingEdition.isRemoved) &&
                                          (string.IsNullOrWhiteSpace(this.SearchQuery) || printingEdition.Title.ToLower().Contains(this.SearchQuery.ToLower()) || printingEdition.AuthorInBooks.Any(aib => aib.Author.Name.ToLower().Contains(this.SearchQuery.ToLower())));
            }
        }
    }
}
