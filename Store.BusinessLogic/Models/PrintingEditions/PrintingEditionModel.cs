﻿using Store.BusinessLogic.Models.Base;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.PrintingEditions
{
    public class PrintingEditionModel : BaseModel
    {
        public IList<PrintingEditionModelItem> Items = new List<PrintingEditionModelItem>();
    }
}
