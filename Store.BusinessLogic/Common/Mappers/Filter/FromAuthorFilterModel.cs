using Store.BusinessLogic.Models.Filters;
using Store.DataAccess.Models.EFFilters;

namespace Store.BusinessLogic.Common.Mappers.Filter
{
    public static partial class FilterMapperExtension
    {
        public static FilterModel<DataAccess.Entities.Author> MapToEFFilterModel(this AuthorFilterModel filterBL)
        {
            var filterDAL = new FilterModel<DataAccess.Entities.Author>();
            filterDAL.SortProperty = filterBL.SortProperty;
            filterDAL.IsAscending = filterBL.IsAscending;
            filterDAL.StartIndex = filterBL.StartIndex;
            filterDAL.Quantity = filterBL.Quantity;
            filterDAL.Predicate = author => (string.IsNullOrWhiteSpace(filterBL.Name) || author.Name.ToLower().Contains(filterBL.Name.ToLower())) &&
                                            (!author.isRemoved);
            return filterDAL;
        }
    }
}
