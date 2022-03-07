using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Common.Dependency;
using Common.SiteSettings;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class EmailSender : IEmailSender , IScopedDependency
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptionsSnapshot<SiteSettings> siteSettings)
        {
            _emailSettings = siteSettings.Value.EmailSettings;
        }
        public Task SendEmailAsync(string toEmail, string subject, string message, bool isMessageHtml = false)
        {
            SmtpClient client = new SmtpClient();

            client.Host = _emailSettings.Host;
            client.Port = _emailSettings.Port;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = _emailSettings.Ssl;
            client.UseDefaultCredentials = _emailSettings.UseDefaultCredentials;
            client.Credentials = new NetworkCredential()
            {
                UserName = _emailSettings.UserName,
                Password = _emailSettings.Password
            };

            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(_emailSettings.UserName, _emailSettings.DisplayName);
            MailAddress emto = new MailAddress(toEmail);

            msg.To.Add(emto);
            msg.Subject = subject;
            msg.Body = message;
            msg.Priority = MailPriority.High;

            client.Send(msg);

            return Task.CompletedTask;
        }
    }
}
