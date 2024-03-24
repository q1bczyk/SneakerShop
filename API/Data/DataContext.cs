using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DataContext(DbContextOptions options) : base(options){}
        public DbSet<Contact> Contacts{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithMany(r => r.Roles)
                .UsingEntity<UserRole>();

            builder.Entity<Contact>()
                .HasOne(c => c.User)
                .WithMany(c => c.Contacts)
                .HasForeignKey(c => c.UserId)
                .IsRequired();
            
            builder.Entity<IdentityUserLogin<string>>().HasNoKey();
            builder.Entity<IdentityUserToken<string>>().HasNoKey();
        }

    }
}