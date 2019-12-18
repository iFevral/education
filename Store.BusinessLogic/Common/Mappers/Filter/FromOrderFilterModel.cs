using System.Linq;
using Store.BusinessLogic.Models.Filters;
using Store.DataAccess.Models.EFFilters;

namespace Store.BusinessLogic.Common.Mappers.Filter
{
    public static partial class FilterMapperExtension
    {
        public static FilterModel<DataAccess.Entities.Order> MapToEFFilterModel(this OrderFilterModel filterBL)
        {
            var filterDAL = new FilterModel<DataAccess.Entities.Order>();
            filterDAL.SortProperty = filterBL.SortProperty;
            filterDAL.IsAscending = filterBL.IsAscending;
            filterDAL.StartIndex = filterBL.StartIndex;
            filterDAL.Quantity = filterBL.Quantity;
            
            filterDAL.Predicate = order => (filterBL.Statuses != null && filterBL.Statuses.Count > 0 && filterBL.Statuses.Any(status => status == (int)order.Status)) &&
                                           (filterBL.UserId == null || filterBL.UserId == order.UserId) &&
                                           (!order.isRemoved);
            
            return filterDAL;
        }
    }
}
