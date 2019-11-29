using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Roles
{
    public class UserRoleModelItem : BaseModel
    {
        public string Rolename { get; set; }
        public string Username { get; set; }
    }
}
