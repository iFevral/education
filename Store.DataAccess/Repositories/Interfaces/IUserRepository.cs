using System;
using System.Collections.Generic;
using System.Text;

namespace Store.DataAccess.Repositories
{
    public interface IUserRepository : IRepository<Users>
    {
        void CheckUserRoles();
        
    }
}
