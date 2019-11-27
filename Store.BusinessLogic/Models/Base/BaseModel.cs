using System;
using System.Collections.Generic;
using System.Text;

namespace Store.BusinessLogic.Models.Base
{
    public class BaseModel
    {
        public ICollection<string> Errors = new List<string>();
    }
}