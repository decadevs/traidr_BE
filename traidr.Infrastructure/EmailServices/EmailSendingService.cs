using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using traidr.Application.IServices;

namespace traidr.Infrastructure.EmailServices
{
    public class EmailSendingService : IEmailSendingService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailSendingService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            
            var mail = new MailMessage
            {
                From = new MailAddress(_smtpSettings.FromEmail, _smtpSettings.FromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true // Set to true if you're sending HTML content
            };

            mail.To.Add(new MailAddress(toEmail));

            using (var smtpClient = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port))
            {
                smtpClient.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
                smtpClient.EnableSsl = true; // Gmail requires SSL

                await smtpClient.SendMailAsync(mail);
            }

        }
    }
}
