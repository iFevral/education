using Store.BusinessLogic.Models.Users;

namespace Store.BusinessLogic.Common.Mappers.User.SignUp
{
    public static partial class SignUpMapperExtension
    {
        public static DataAccess.Entities.User MapToEntity(this SignUpModel model)
        {
            var entity = new DataAccess.Entities.User();
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.UserName = model.Email;
            entity.Email = model.Email;
            return entity;
        }
    }
}
