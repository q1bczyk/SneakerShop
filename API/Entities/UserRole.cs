using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class UserRole : IdentityUserRole<string>
    {
        public Role Role { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}