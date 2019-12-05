﻿using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Store.BusinessLogic.Helpers.Interface
{
    public interface IEmailHelper
    {
        public void Configure(IConfigurationSection smtpConfig);
        public Task Send(string recipients, string subject, string body);
    }
}
