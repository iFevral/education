using System.Collections.Generic;
using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.Authors;

namespace Store.BusinessLogic.Models.PrintingEditions
{
    public class PrintingEditionModelItem : BaseModel
    {
        public int Id { get; set; } //id => long
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; } //todo enum
        public string Type { get; set; } //todo enum
        public IList<AuthorModelItem> Authors { get; set; } = new List<AuthorModelItem>();
    }
}
