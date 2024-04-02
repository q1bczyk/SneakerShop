using System.Net;
using System.Net.Mail;
using API.Helpers;
using API.Interfaces;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient stmp;
        private readonly string EmailSender;
        private readonly int Port;
        private readonly string Key;

        public EmailService(IOptions<EmailConfig> config)
        {
            EmailSender = config.Value.EmailSender;
            Port = config.Value.Port;
            Key = config.Value.Key;

            stmp = new SmtpClient("stmp.gmail.com")
            {
                Port = Port,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(EmailSender, Key),
                EnableSsl = true,
            };
        }

        public async Task<bool> SendEmailAsync(string recipient)
        {
            MailMessage mailMessage = new MailMessage(EmailSender, recipient)
            {
                Subject = "Sneakers Shop - Confirm Account",
                Body = "Kliknije w link żeby potwierdzić swoje konto: "
            };

            await stmp.SendMailAsync(mailMessage);

            return true;
        }
    }
}