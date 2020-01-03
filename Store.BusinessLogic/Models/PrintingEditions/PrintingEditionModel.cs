using Store.BusinessLogic.Models.Base;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.PrintingEditions
{
    public class PrintingEditionModel : BaseModel
    {
        public IList<PrintingEditionModelItem> Items { get; set; } = new List<PrintingEditionModelItem>();
        public int Counter { get; set; } = 0;
    }
}
