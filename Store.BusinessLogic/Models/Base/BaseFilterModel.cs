using Store.DataAccess.Entities.Enums;

namespace Store.BusinessLogic.Models.Base
{
    public abstract class BaseFilterModel
    {
        public Enums.Filter.SortProperty SortProperty { get; set; } = Enums.Filter.SortProperty.Id;
        public bool IsAscending { get; set; } = true;
        public int StartIndex { get; set; } = 0;
        public int Quantity { get; set; } = 0;
    }
}
