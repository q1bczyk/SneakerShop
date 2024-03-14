using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly DataContext context;

        public ContactRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<Contact> AddContactAsync(Contact contact)
        {
            await context.AddAsync(contact);
            await SaveAllAsync();
            return contact;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Update(Contact contact)
        {
            context.Entry(contact).State = EntityState.Modified;
        }
    }
}