using Store.BusinessLogic.Common.Mappers.Interface;
using Store.BusinessLogic.Models.Users;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Common.Mappers
{
    public class SignUpMapper : IMapper<Users, SignUpModel>
    {
        public Users Map(SignUpModel model, Users entity)
        {
            entity.FirstName = string.IsNullOrWhiteSpace(model.Firstname)
                ? entity.FirstName
                : model.Firstname;

            entity.LastName = string.IsNullOrWhiteSpace(model.Lastname)
                ? entity.LastName
                : model.Lastname;

            entity.UserName = string.IsNullOrWhiteSpace(model.Email)
                ? entity.UserName
                : model.Email;

            entity.Email = string.IsNullOrWhiteSpace(model.Email)
                ? entity.Email
                : model.Email;

            return entity;
        }

        public SignUpModel Map(Users entity, SignUpModel model)
        {
            return model;
        }
    }
}
