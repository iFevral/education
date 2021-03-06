﻿using Store.BusinessLogic.Models.Base;

namespace Store.BusinessLogic.Models.Users
{
    public class EmailConfirmationModel : BaseModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
