using System.Collections.Generic;
using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.Authors;
using Store.DataAccess.Entities.Enums;

namespace Store.BusinessLogic.Models.PrintingEditions
{
    public class PrintingEditionModelItem : BaseModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Enums.PrintingEditions.Currencies? Currency { get; set; }
        public Enums.PrintingEditions.Types? Type { get; set; }
        public IList<AuthorModelItem> Authors { get; set; } = new List<AuthorModelItem>();
    }
}
