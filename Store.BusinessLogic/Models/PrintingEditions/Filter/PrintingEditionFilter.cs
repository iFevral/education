using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.PrintingEditions
{
    public class PrintingEditionFilter : BaseFilterModel
    {
        public string Title { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string Author { get; set; }
        public IList<int> Types { get; set; }

        public Expression<Func<DataAccess.Entities.PrintingEditions, bool>> Predicate
        {
            get
            {
                return pe => (string.IsNullOrWhiteSpace(this.Title) || pe.Title.ToLower().Contains(this.Title.ToLower())) &&
                             (this.Types == null || this.Types.Count == 0 || this.Types.Any(t => t == (int)pe.Type)) &&
                             (this.MinPrice == null || pe.Price >= this.MinPrice) &&
                             (this.MaxPrice == null || pe.Price <= this.MaxPrice) &&
                             (!pe.isRemoved) &&
                             (string.IsNullOrWhiteSpace(this.Author) || pe.AuthorInBooks
                                                                          .Any(aib =>
                                                                               aib.Author.Name.ToLower().Contains(this.Author.ToLower())));
            }
        }
    }
}
