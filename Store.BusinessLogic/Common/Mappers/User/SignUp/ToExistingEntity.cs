using Store.BusinessLogic.Models.Users;

namespace Store.BusinessLogic.Common.Mappers.User.SignUp
{
    public static partial class SignUpMapperExtension
    {
        public static DataAccess.Entities.User MapToEntity(this SignUpModel model, DataAccess.Entities.User entity)
        {
            entity.FirstName = string.IsNullOrWhiteSpace(model.FirstName)
                ? entity.FirstName
                : model.FirstName;

            entity.LastName = string.IsNullOrWhiteSpace(model.LastName)
                ? entity.LastName
                : model.LastName;

            entity.Email = string.IsNullOrWhiteSpace(model.Email)
                ? entity.Email
                : model.Email;

            entity.Avatar = string.IsNullOrWhiteSpace(model.Avatar)
                ? entity.Avatar
                : model.Avatar;

            return entity;
        }
    }
}
