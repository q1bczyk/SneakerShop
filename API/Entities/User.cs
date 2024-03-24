using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class User : IdentityUser
    {
        public List<Role> Roles { get; set;} = new();
        public List<Contact> Contacts { get; set; } = new();
    }
}