﻿using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Users
{
    public class SignInModel : BaseModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
