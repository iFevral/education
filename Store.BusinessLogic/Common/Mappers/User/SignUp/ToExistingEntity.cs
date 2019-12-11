using Store.BusinessLogic.Models.Users;

namespace Store.BusinessLogic.Common.Mappers.User.SignUp
{
    public static partial class SignUpMapperExtension
    {
        public static DataAccess.Entities.User MapToEntity(this SignUpModel model, DataAccess.Entities.User entity)
        {
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Email = model.Email;

            return entity;
        }
    }
}
