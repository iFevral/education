using System.Collections.Generic;
using Store.DataAccess.Entities;
using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Common.Mappers.Interface;

namespace Store.BusinessLogic.Common.Mappers
{
    public class UserMapper : IMapper<Users, UserModelItem>
    {
        public Users Map(UserModelItem model, Users entity)
        {
            entity.FirstName = string.IsNullOrWhiteSpace(model.Firstname)
                ? entity.FirstName
                : model.Firstname;

            entity.LastName = string.IsNullOrWhiteSpace(model.Lastname)
                ? entity.LastName
                : model.Lastname;

            entity.Email = string.IsNullOrWhiteSpace(model.Email)
                ? entity.Email
                : model.Email;
            
            return entity;
        }

        public UserModelItem Map(Users entity, UserModelItem model)
        {
            model.Id = entity.Id;
            model.Firstname = entity.FirstName;
            model.Lastname = entity.LastName;
            model.Email = entity.Email;
            model.EmailConfirmed = entity.EmailConfirmed;
            model.Roles = new List<string>();
            foreach (var role in entity.UserInRoles)
            {
                model.Roles.Add(role.Role.Name);
            }

            return model;
        }
    }
}
