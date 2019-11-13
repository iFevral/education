using System.Net;
using System.Net.Mail;

namespace Store.BusinessLogic.Helpers
{
    public class EmailHelper
    {
        public void Send()
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("test849302@gmail.com", "Qweasd!23");
            client.EnableSsl = true;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("test849302@gmail.com");
            mailMessage.To.Add("daxit21295@iwebtm.com");
            mailMessage.Body = "body";
            mailMessage.Subject = "subject";
            client.Send(mailMessage);
        }
    }
}
