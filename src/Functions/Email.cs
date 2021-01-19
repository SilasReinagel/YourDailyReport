using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SilasReinagel.YourDailyReport
{
  public class Email
  {
      private readonly EmailConfigString _cfg;

      public Email() 
          : this(new EmailConfigString(new EnvironmentVariable("YourDailyReportMailConfig"))) {}

      public Email(EmailConfigString cfg) => _cfg = cfg;

      public async Task Send(string toAddress, string subject, string body)
      {
         var message = new MailMessage();  
         var smtp = new SmtpClient();  
         message.From = new MailAddress(_cfg.From);  
         message.To.Add(new MailAddress(toAddress));  
         message.Subject = subject;
         message.IsBodyHtml = false;
         message.Body = body;
         
         smtp.Port = _cfg.SmtpPort;
         smtp.Host = _cfg.SmtpHost; 
         smtp.EnableSsl = true;  
         smtp.UseDefaultCredentials = false;
         smtp.Credentials = new NetworkCredential(_cfg.Username, _cfg.Password);  
         smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
         
         await smtp.SendMailAsync(message);
      }
  }

  public class EmailConfigString
  {
      private readonly string _cfg;

      public EmailConfigString(string cfg) => _cfg = cfg;

      public string SmtpHost => _cfg.Split('|')[0].Split(':')[0];
      public int SmtpPort => int.Parse(_cfg.Split('|')[0].Split(':')[1]);
      public string From => _cfg.Split('|')[1];
      public string Username => _cfg.Split('|')[2];
      public string Password => _cfg.Split('|')[3];
  }
}
