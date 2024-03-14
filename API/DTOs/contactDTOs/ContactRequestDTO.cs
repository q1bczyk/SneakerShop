using System.ComponentModel.DataAnnotations;

namespace API.DTOs.contactDTOs
{
    public class ContactRequestDTO
    {
        [Required, MinLength(2)]
        public string Name { get; set; }
        [Required, MinLength(2)]
        public string LastName { get; set; }
        [Required, MinLength(8), MaxLength(8)]
        public string PhoneNumber { get; set; }
        [Required, MinLength(2)]
        public string Street { get; set; }
        [Required, MinLength(8), MaxLength(8)]
        public string PostalCode { get; set; }
    }
}