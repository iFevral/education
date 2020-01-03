using System.Collections.Generic;

namespace Store.DataAccess.Models
{
    public class DataModel<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public int Counter { get; set; }
    }
}
