using System.Linq;
using Store.BusinessLogic.Models.Filters;
using Store.DataAccess.Models.EFFilters;

namespace Store.BusinessLogic.Common.Mappers.Filter
{
    public static partial class FilterMapperExtension
    {
        public static EFFilterModel<DataAccess.Entities.PrintingEdition> MapToEFFilterModel(this PrintingEditionFilterModel filterBL)
        {
            var filterDAL = new EFFilterModel<DataAccess.Entities.PrintingEdition>();
            filterDAL.SortProperty = filterBL.SortProperty;
            filterDAL.IsAscending = filterBL.IsAscending;
            filterDAL.StartIndex = filterBL.StartIndex;
            filterDAL.Quantity = filterBL.Quantity;

            filterDAL.Predicate = printingEdition => (filterBL.Types != null && filterBL.Types.Count > 0 && filterBL.Types.Any(t => t == (int)printingEdition.Type)) &&
                                           (filterBL.MinPrice == null || printingEdition.Price >= filterBL.MinPrice) &&
                                           (filterBL.MaxPrice == null || printingEdition.Price <= filterBL.MaxPrice) &&
                                           (!printingEdition.isRemoved) &&
                                           (string.IsNullOrWhiteSpace(filterBL.SearchQuery) ||
                                            printingEdition.Title.ToLower().Contains(filterBL.SearchQuery.ToLower()) ||
                                            printingEdition.AuthorInBooks.Any(aib => aib.Author.Name.ToLower().Contains(filterBL.SearchQuery.ToLower())));

            return filterDAL;
        }
    }
}