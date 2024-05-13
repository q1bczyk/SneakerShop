namespace API.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendConfirmEmailAsync(string recipient, string userId, string token);
        Task<bool> SendPasswordResetEmailAsync(string recipient, string userId, string token);
        Task<bool> SendOrderConfirmationAsync(string recipient, string orderId);
    }
}