using Store.BusinessLogic.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        public UserModel GetAllUsers();
    }
}
