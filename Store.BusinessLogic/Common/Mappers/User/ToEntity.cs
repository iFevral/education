using Store.BusinessLogic.Models.Users;

namespace Store.BusinessLogic.Common.Mappers.User
{
    public static partial class UserMapperExtension
    {
        public static DataAccess.Entities.User MapToEntity(this UserModelItem model, DataAccess.Entities.User entity)
        {
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Email = model.Email;

            return entity;
        }
    }
}
