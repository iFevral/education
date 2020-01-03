using System.Collections.Generic;
using Store.BusinessLogic.Models.Base;
using Store.BusinessLogic.Models.PrintingEditions;

namespace Store.BusinessLogic.Models.Authors
{
    public class AuthorModelItem : BaseModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IList<PrintingEditionModelItem> PrintingEditions { get; set; } = new List<PrintingEditionModelItem>();
    }
}