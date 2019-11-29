using System.Collections.Generic;
using Store.DataAccess.Entities;
using Store.BusinessLogic.Models.Users;

namespace Store.BusinessLogic.Common.Mappers.User
{
    public class UserMapper : IMapper<Users, UserModelItem>
    {
        public Users Map(UserModelItem src, Users dest)
        {
            dest.Id = string.IsNullOrWhiteSpace(src.Id)
                ? dest.Id
                : src.Id;

            dest.FirstName = string.IsNullOrWhiteSpace(src.Firstname)
                ? dest.FirstName
                : src.Firstname;

            dest.LastName = string.IsNullOrWhiteSpace(src.Lastname)
                ? dest.LastName
                : src.Lastname;

            dest.Email = string.IsNullOrWhiteSpace(src.Email)
                ? dest.Email
                : src.Email;

            return dest;
        }

        public UserModelItem Map(Users src, UserModelItem dest)
        {
            dest.Id = src.Id;
            dest.Firstname = src.FirstName;
            dest.Lastname = src.LastName;
            dest.Email = src.Email;

            dest.Roles = new List<string>();
            foreach (var role in src.UserInRoles)
            {
                dest.Roles.Add(role.Role.Name);
            }

            return dest;
        }
    }
}
