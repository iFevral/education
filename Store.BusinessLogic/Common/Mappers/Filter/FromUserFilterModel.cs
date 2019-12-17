using System.Linq;
using Store.BusinessLogic.Models.Filters;
using Store.DataAccess.Models.EFFilters;

namespace Store.BusinessLogic.Common.Mappers.Filter
{
    public static partial class FilterMapperExtension
    {
        public static FilterModel<DataAccess.Entities.User> MapToEFFilterModel(this UserFilterModel filterBL)
        {
            var filterDAL = new FilterModel<DataAccess.Entities.User>(); 
            filterDAL.SortProperty = filterBL.SortProperty;
            filterDAL.IsAscending = filterBL.IsAscending;
            filterDAL.StartIndex = filterBL.StartIndex;
            filterDAL.Quantity = filterBL.Quantity;

            filterDAL.Predicate = user => (string.IsNullOrWhiteSpace(filterBL.SearchQuery) || (user.FirstName + " " + user.LastName).ToLower().Contains(filterBL.SearchQuery.ToLower())) &&
                               (filterBL.LockStatuses != null && filterBL.LockStatuses.Count > 0 && filterBL.LockStatuses.Any(s => s == user.LockoutEnabled)) &&
                               (!user.isRemoved);

            return filterDAL;
        }
    }
}
