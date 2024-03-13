using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MaxLength(36)]
        public string Id { get; set; }
        public byte [] Password { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Role { get; set; }
        public List<Contact> Contacts { get; set; } = new();
    }
}