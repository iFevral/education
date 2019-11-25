
namespace Store.BusinessLogic.Models.Users
{
    public class UserFilter
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        public bool? LockoutEnabled { get; set; }
        public string Role { get; set; }
    }
}
