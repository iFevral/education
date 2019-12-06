using Store.BusinessLogic.Models.Filters;
using Store.DataAccess.Models.EFFilters;

namespace Store.BusinessLogic.Common.Mappers.Filter
{
    public static partial class FilterMapperExtension
    {
        public static EFFilterModel<DataAccess.Entities.Author> MapToEFFilterModel(this AuthorFilterModel filterBL)
        {
            var filterDAL = new EFFilterModel<DataAccess.Entities.Author>();
            filterDAL.SortProperty = filterBL.SortProperty;
            filterDAL.IsAscending = filterBL.IsAscending;
            filterDAL.StartIndex = filterBL.StartIndex;
            filterDAL.Quantity = filterBL.Quantity;
            return filterDAL;
        }
    }
}
