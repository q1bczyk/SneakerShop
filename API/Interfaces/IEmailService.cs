namespace API.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendConfirmEmailAsync(string recipient, string userId, string token);
    }
}