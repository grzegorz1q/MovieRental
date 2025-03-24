using MovieRental.Domain.Interfaces;
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
        public Task SendEmail(string email, string subject, string message)
        {
            var mail = "movierentalpcz@gmail.com";
            var password = "otgc wzww xzed tuxs";

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
