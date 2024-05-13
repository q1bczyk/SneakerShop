using API.Interfaces;

namespace API.Extensions.OrderExtensions
{
    public class CheckContactExtension
    {
        private readonly IContactRepository _contactRepository;

        public CheckContactExtension(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<bool> CheckContact(string userId, string contactId)
        {
            var contact = await _contactRepository.GetContactByIdAsync(contactId);
            
            if(contact == null || contact.UserId != userId)
                return false;
            
            return true;
        }
    }
}