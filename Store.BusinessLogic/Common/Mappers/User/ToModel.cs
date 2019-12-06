using System.Collections.Generic;
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
            model.Roles = new List<string>();

            foreach (var role in entity.UserInRoles)
            {
                model.Roles.Add(role.Role.Name);
            }

            return model;
        }
    }
}
