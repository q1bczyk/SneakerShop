using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(User user);
        Task<User> AddUserAsync(User user);
        Task<User> GetUserByEmail(string email);
        Task<bool> SaveAllAsync();
    }
}