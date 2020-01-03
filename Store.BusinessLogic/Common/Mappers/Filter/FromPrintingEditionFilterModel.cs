using System.Collections.Generic;
using System.Linq;
using Store.BusinessLogic.Extensions.Currency;

namespace Store.BusinessLogic.Common.Mappers.Filter
{
    public static partial class FilterMapperExtension
    {
        public static DataAccess.Models.Filters.PrintingEditionFilterModel MapToEFFilterModel(this Models.Filters.PrintingEditionFilterModel filterBL)
        {
            var filterDAL = new DataAccess.Models.Filters.PrintingEditionFilterModel();
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
                                            printingEdition.AuthorInPrintingEditions.Any(aib => aib.Author.Name.ToLower().Contains(filterBL.SearchQuery.ToLower())));

            filterDAL.SearchQuery = filterBL.SearchQuery;
            
            filterDAL.MinPrice = filterBL.MinPrice == null
                ? 0
                : filterBL.MinPrice;

            filterDAL.MaxPrice = filterBL.MaxPrice == null
                ? 100000000
                : filterBL.MaxPrice;

            filterDAL.Types = filterBL.Types.Count() == 0
                ? new List<int>() { 0 }
                : filterBL.Types;

            return filterDAL;
        }
    }
}