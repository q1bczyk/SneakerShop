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
        private readonly string BaseUrl;

        public EmailService(IOptions<EmailConfig> config)
        {
            EmailSender = config.Value.EmailSender;
            Port = config.Value.Port;
            Key = config.Value.Key;
            BaseUrl = $"{config.Value.AppUrl}Auth/";
             
            stmp = new SmtpClient("smtp.gmail.com")
            {
                Port = Port,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(EmailSender, Key),
                EnableSsl = true,
            };
        }

        public async Task<bool> SendConfirmEmailAsync(string recipient, string userId, string token)
        {
            string confirmationLink = $"{BaseUrl}ConfirmEmail?userId={userId}&token={token}";

            MailMessage mailMessage = new MailMessage(EmailSender, recipient)
            {
                Subject = "Sneakers Shop - Confirm Account",
                Body = "Kliknij w link żeby potwierdzić swoje konto: " + confirmationLink,
            };

            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            await stmp.SendMailAsync(mailMessage);

            return true;
        }

        public async Task<bool> SendPasswordResetEmailAsync(string recipient, string userId, string token)
        {
            string passwordResetLink = $"{BaseUrl}ChangePassword?userId={userId}&token={token}";

            MailMessage mailMessage = new MailMessage(EmailSender, recipient)
            {
                Subject = "Sneakers Shop - Password Reset",
                Body = "Kliknij w link żeby zresetować hasło: " + passwordResetLink,
            };

            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            await stmp.SendMailAsync(mailMessage);

            return true;
        }

    }
}