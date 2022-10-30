using System.Collections.Generic;
using System.Net.Mail;
using IFramework.Infrastructure.Utility.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace IFramework.Infrastructure.Utility.Notification.Email
{
    /// <summary>
    /// Email ile bildirim gönderme işlemi yapılır.
    /// </summary>
    public class EmailNotification : IEmailNotification
    {
        private IOptions<IFrameworkConfig> _configuration;
        public EmailNotification(IOptions<IFrameworkConfig> configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Tek bir kullanıcıya email gönderme işlemi yapılır.
        /// </summary>
        /// <param name="toUser">Email gönderilecek mail adresi verilmelidir.</param>
        /// <param name="content">Emailde gönderilmesi istenilen mail içeriği verilmelidir.</param>
        public void Send(string toUser, string content)
        {
            SmtpClient smtpClient = new SmtpClient(_configuration.Value.EmailQueueHost) { UseDefaultCredentials = true };
            MailMessage message = new MailMessage
            {
                From = new MailAddress(_configuration.Value.FromEmail, _configuration.Value.FromEmailName),
                IsBodyHtml = true,
                Subject = _configuration.Value.EmailSubject,
                Body = content
            };
            message.To.Add(new MailAddress(toUser));
            smtpClient.Send(message);
            smtpClient.Dispose();
            message.Dispose();
        }

        /// <summary>
        /// Tek bir kullanıcıya email gönderme işlemi yapılır.
        /// </summary>
        /// <param name="toUsers">Email gönderilecek mail adreslerinin listesi verilmelidir.</param>
        /// <param name="content">Emailde gönderilmesi istenilen mail içeriği verilmelidir.</param>
        public void Send(List<string> toUsers, string content)
        {
            SmtpClient smtpClient = new SmtpClient(_configuration.Value.EmailQueueHost) { UseDefaultCredentials = true };
            MailMessage message = new MailMessage
            {
                From = new MailAddress(_configuration.Value.FromEmail, _configuration.Value.FromEmailName),
                IsBodyHtml = true,
                Subject = _configuration.Value.EmailSubject,
                Body = content
            };
            foreach (string toUser in toUsers)
            {
                message.To.Add(new MailAddress(toUser));
                smtpClient.Send(message);

                message.To.Remove(new MailAddress(toUser));
                System.Threading.Thread.Sleep(1000);
            }
            smtpClient.Dispose();
            message.Dispose();
        }
    }
}
