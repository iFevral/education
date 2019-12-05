using Store.BusinessLogic.Models.Base;
using Store.DataAccess.Models;

namespace Store.BusinessLogic.Common.Mappers.Filter
{
    public static partial class FilterMapperExtension
    {
        public static FilterModel<T> MapToDataAccessModel<T>(this BaseFilterModel<T> filterBL) where T : class
        {
            var filterDAL = new FilterModel<T>();
            filterDAL.SortProperty = filterBL.SortProperty;
            filterDAL.SortWay = filterBL.SortWay;
            filterDAL.StartIndex = filterBL.StartIndex;
            filterDAL.Quantity = filterBL.Quantity;
            filterDAL.Predicate = filterBL.Predicate;

            return filterDAL;
        }
    }
}
