using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Store.BusinessLogic.Helpers.Interface;

namespace Store.BusinessLogic.Helpers
{
    public class EmailHelper : IEmailHelper//todo add interface, use DI, use options
    {
        private bool _smtpSsl;
        private int _smtpPort;
        private string _smtpEmail;
        private string _smtpServer;
        private string _smtpPassword;

        public void Configure(IConfigurationSection smtpConfig)
        {
            _smtpSsl = smtpConfig.GetValue<bool>("SmtpSsl");
            _smtpPort = smtpConfig.GetValue<int>("SmtpPort");
            _smtpEmail = smtpConfig.GetValue<string>("SmtpEmail");
            _smtpServer = smtpConfig.GetValue<string>("SmtpServer");
            _smtpPassword = smtpConfig.GetValue<string>("SmtpPassword");
        }

        public async Task Send(string recipients, //todo async
                                string subject,
                                string body)
        {
            //todo using
            using (SmtpClient client = new SmtpClient(_smtpServer, _smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_smtpEmail, _smtpPassword);
                client.EnableSsl = Convert.ToBoolean(_smtpSsl);

                MailMessage message = new MailMessage();
                message.From = new MailAddress(_smtpServer);
                message.To.Add(recipients);
                message.Body = body;
                message.Subject = subject;
                await client.SendMailAsync(message);
            }
        }
    }
}