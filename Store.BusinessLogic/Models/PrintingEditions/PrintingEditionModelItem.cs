using Store.BusinessLogic.Models.Authors;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.PrintingEditions
{
    public class PrintingEditionModelItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }

        public IList<AuthorModelItem> Authors { get; set; } = new List<AuthorModelItem>();
    }
}
