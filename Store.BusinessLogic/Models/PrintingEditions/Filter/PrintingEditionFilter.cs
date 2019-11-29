using System;
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

        public Expression<Func<DataAccess.Entities.PrintingEditions, bool>> Predicate
        {
            get
            {
                return pe => (string.IsNullOrWhiteSpace(this.Title) || ValidateString(pe.Title).Contains(ValidateString(this.Title))) &&
                                                   (this.MinPrice == null || pe.Price >= this.MinPrice) &&
                                                   (this.MaxPrice == null || pe.Price <= this.MaxPrice) &&
                                                   (string.IsNullOrWhiteSpace(this.Author) || pe.AuthorInBooks
                                                                                                .Where(aib =>
                                                                                                        ValidateString(aib.Author.Name)
                                                                                                        .Contains(ValidateString(this.Author)))
                                                                                                        .Any());
            }
        }
    }
}
