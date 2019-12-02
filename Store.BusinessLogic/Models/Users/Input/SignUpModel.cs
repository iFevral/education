﻿using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Users
{
    public class SignUpModel : BaseModel
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
