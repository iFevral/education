namespace Store.BusinessLogic.Models.Base
{
    public class BaseFilterModel
    {
        public string SortProperty { get; set; } = "Id";
        public string SortWay { get; set; } = "ASC";

        public int StartIndex { get; set; } = 0;
        public int Quantity { get; set; } = 0;
    }
}
