using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class Role : IdentityRole
    {
        public List<User> Users { get; set; } = new();
    }
}