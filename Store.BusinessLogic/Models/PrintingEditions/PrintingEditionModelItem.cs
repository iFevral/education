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
        public decimal Price { get; set; } = 0;
        public string Image { get; set; }
        public Enums.PrintingEditions.Currency? Currency { get; set; }
        public Enums.PrintingEditions.PrintingEditionType? Type { get; set; }
        public IList<AuthorModelItem> Authors { get; set; } = new List<AuthorModelItem>();
    }
}
