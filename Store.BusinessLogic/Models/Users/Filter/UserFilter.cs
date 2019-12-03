using System;
using System.Linq.Expressions;
using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Users
{
    public class UserFilter : BaseFilterModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public bool? IsLockoutEnabled { get; set; }

        public Expression<Func<DataAccess.Entities.Users, bool>> Predicate
        {
            get
            {
                return u => (string.IsNullOrWhiteSpace(this.Firstname) || u.FirstName.ToLower().Contains(this.Firstname.ToLower())) &&
                            (string.IsNullOrWhiteSpace(this.Lastname) || u.LastName.ToLower().Contains(this.Lastname.ToLower())) &&
                            (string.IsNullOrWhiteSpace(this.Username) || u.UserName.ToLower().Contains(this.Username.ToLower())) &&
                            (string.IsNullOrWhiteSpace(this.Email) || u.Email.ToLower().Contains(this.Email.ToLower())) &&
                            (this.IsLockoutEnabled == null || u.LockoutEnabled == this.IsLockoutEnabled);
            }
        }
    }
}
