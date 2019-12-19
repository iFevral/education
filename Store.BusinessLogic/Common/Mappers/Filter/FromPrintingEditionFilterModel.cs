using System.Linq;
using Store.BusinessLogic.Models.Filters;
using Store.DataAccess.Models.EFFilters;
using Store.BusinessLogic.Extensions.Currency;

namespace Store.BusinessLogic.Common.Mappers.Filter
{
    public static partial class FilterMapperExtension
    {
        public static FilterModel<DataAccess.Entities.PrintingEdition> MapToEFFilterModel(this PrintingEditionFilterModel filterBL)
        {
            var filterDAL = new FilterModel<DataAccess.Entities.PrintingEdition>();
            filterDAL.SortProperty = filterBL.SortProperty;
            filterDAL.IsAscending = filterBL.IsAscending;
            filterDAL.StartIndex = filterBL.StartIndex;
            filterDAL.Quantity = filterBL.Quantity;

            decimal minPriceInUSD = 0;
            if(filterBL.MinPrice != null)
            {
                minPriceInUSD = filterBL.MinPrice.ConvertToUSD(filterBL.Currency);
            }

            decimal maxPriceInUSD = 0;
            if (filterBL.MaxPrice != null)
            {
                maxPriceInUSD = filterBL.MaxPrice.ConvertToUSD(filterBL.Currency);
            }

            filterDAL.Predicate = printingEdition => (filterBL.Types != null && filterBL.Types.Count > 0 && filterBL.Types.Any(t => t == (int)printingEdition.Type)) &&
                                           (filterBL.MinPrice == null || printingEdition.Price >= minPriceInUSD) &&
                                           (filterBL.MaxPrice == null || printingEdition.Price <= maxPriceInUSD) &&
                                           (!printingEdition.isRemoved) &&
                                           (string.IsNullOrWhiteSpace(filterBL.SearchQuery) ||
                                            printingEdition.Title.ToLower().Contains(filterBL.SearchQuery.ToLower()) ||
                                            printingEdition.AuthorInBooks.Any(aib => aib.Author.Name.ToLower().Contains(filterBL.SearchQuery.ToLower())));

            return filterDAL;
        }
    }
}