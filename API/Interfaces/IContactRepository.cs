using API.Entities;

namespace API.Interfaces
{
    public interface IContactRepository
    {
        void Update(Contact contact);
        Task<Contact> AddContactAsync(Contact contact);
        Task<Contact> GetContactByIdAsync(string id);
        Task<bool> SaveAllAsync();
    }
}