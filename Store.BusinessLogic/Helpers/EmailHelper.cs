using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Store.BusinessLogic.Helpers
{
    public static class EmailHelper
    {
        public static void Send(string recipients,
                                string subject,
                                string body,
                                IConfiguration configuration)
        {
            SmtpClient client = new SmtpClient(configuration["SmtpServer"],
                                               Convert.ToInt32(configuration["SmtpPort"]));
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(configuration["SmtpEmail"],
                                                       configuration["SmtpPassword"]);
            client.EnableSsl = Convert.ToBoolean(configuration["SmtpSsl"]);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(configuration["SmtpEmail"]);
            message.To.Add(recipients);
            message.Body = body;
            message.Subject = subject;
            client.Send(message);
        }
    }
}
//TODO: Доделать EmailHelper