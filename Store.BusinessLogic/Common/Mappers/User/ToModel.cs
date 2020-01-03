using Store.BusinessLogic.Models.Users;

namespace Store.BusinessLogic.Common.Mappers.User
{
    public static partial class UserMapperExtension
    {
        public static UserModelItem MapToModel(this DataAccess.Entities.User entity)
        {
            var model = new UserModelItem();
            model.Id = entity.Id;
            model.FirstName = entity.FirstName;
            model.LastName = entity.LastName;
            model.Email = entity.Email;
            model.IsLocked = entity.LockoutEnabled;
            model.Avatar = string.IsNullOrWhiteSpace(entity.Avatar) 
                ? Constants.Constants.Images.DefaultUserAvatar 
                : entity.Avatar;

            return model;
        }
    }
}
