using Microsoft.AspNetCore.Identity;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Initialization
{
    public static class DatabaseInitialization
    {
        public static ApplicationContext ApplicationContext { get; set; }
        public static UserManager<Users> UserManager { get; set; }
        public static RoleManager<IdentityRole> RoleManager { get; set; }
    }
}

