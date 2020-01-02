using System.Linq;
using System.Collections.Generic;

namespace Store.BusinessLogic.Common.Mappers.Filter
{
    public static partial class FilterMapperExtension
    {
        public static DataAccess.Models.Filters.OrderFilterModel MapToEFFilterModel(this Models.Filters.OrderFilterModel filterBL)
        {
            var filterDAL = new DataAccess.Models.Filters.OrderFilterModel();
            filterDAL.SortProperty = filterBL.SortProperty;
            filterDAL.IsAscending = filterBL.IsAscending;
            filterDAL.StartIndex = filterBL.StartIndex;
            filterDAL.Quantity = filterBL.Quantity;
            
            filterDAL.Predicate = order => (filterBL.Statuses != null && filterBL.Statuses.Count > 0 && filterBL.Statuses.Any(status => status == (int)order.Status)) &&
                                           (filterBL.UserId == null || filterBL.UserId == order.UserId) &&
                                           (!order.isRemoved);

            filterDAL.UserId = filterBL.UserId;
            filterDAL.Statuses = filterBL.Statuses.Count() == 0
                ? new List<int>() { 0 }
                : filterBL.Statuses;

            return filterDAL;
        }
    }
}
