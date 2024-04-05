namespace API.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string recipient, string confirmationLink);
    }
}