using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options){}
        
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Contact>()
                .HasOne(c => c.User)
                .WithMany(c => c.Contacts)
                .HasForeignKey(c => c.UserId)
                .IsRequired();
        }

    }
}