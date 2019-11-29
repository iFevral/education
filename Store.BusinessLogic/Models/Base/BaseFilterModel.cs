using System;
using System.Collections.Generic;
using System.Text;

namespace Store.BusinessLogic.Models.Base
{
    public class BaseFilterModel
    {
        protected string ValidateString(string str)
        {
            return str.Trim().ToLower();
        }
    }
}
