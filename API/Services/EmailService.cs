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

            stmp = new SmtpClient("smtp.gmail.com")
            {
                Port = Port,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(EmailSender, Key),
                EnableSsl = true,
            };
        }

        public async Task<bool> SendEmailAsync(string recipient, string confirmationLink)
        {
            MailMessage mailMessage = new MailMessage(EmailSender, recipient)
            {
                Subject = "Sneakers Shop - Confirm Account",
                Body = "Kliknij w link żeby potwierdzić swoje konto: " + confirmationLink
            };

            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            await stmp.SendMailAsync(mailMessage);

            return true;
        }
    }
}