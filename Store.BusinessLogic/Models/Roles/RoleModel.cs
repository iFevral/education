using Store.BusinessLogic.Models.Base;
using System.Collections.Generic;

namespace Store.BusinessLogic.Models.Roles
{
    public class RoleModel : BaseModel
    {
        public IList<RoleModelItem> Roles = new List<RoleModelItem>();
        public IList<UserRoleModelItem> Users = new List<UserRoleModelItem>();
    }
}
