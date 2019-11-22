﻿using System.Collections.Generic;
using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Authors
{
    public class AuthorModel : BaseModel
    {
        public IList<AuthorModelItem> Authors = new List<AuthorModelItem>();
    }
}
