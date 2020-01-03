using System.Collections.Generic;
using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Authors
{
    public class AuthorModel : BaseModel
    {
        public IList<AuthorModelItem> Items { get; set; } = new List<AuthorModelItem>();
        public int Counter { get; set; } = 0;
    }
}
