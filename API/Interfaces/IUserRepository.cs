using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(User user);
        Task<User> AddUserAsync(User user);
        Task<bool> SaveAllAsync();
    }
}