using Microsoft.Extensions.Options;
using MovieRental.Domain.Interfaces;
using MovieRental.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly GmailOptions _gmailOptions;
        public EmailService(IOptions<GmailOptions> gmailOptions)
        {
            _gmailOptions = gmailOptions.Value;
        }
        public Task SendEmail(string email, string subject, string message)
        {
            var mail = _gmailOptions.Email;
            var password = _gmailOptions.Password;

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, password)
            };
            return client.SendMailAsync(
                new MailMessage(from: mail,
                                to: email,
                                subject,
                                message));
        }
    }
}
