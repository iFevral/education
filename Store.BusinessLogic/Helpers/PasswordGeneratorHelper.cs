using Store.BusinessLogic.Common.Constants;
using Store.BusinessLogic.Helpers.Interface;
using System;
using System.Linq;

namespace Store.BusinessLogic.Helpers
{
    public class PasswordGeneratorHelper : IPasswordGeneratorHelper
    {
        public string GeneratePassword()
        {
            Random random = new Random();
            var password = new string(Enumerable.Repeat(Constants.PasswordGeneratorSettings.chars, Constants.PasswordGeneratorSettings.size)
                                                .Select(s => s[random.Next(s.Length)]).ToArray());
            return password;
        }
    }
}
