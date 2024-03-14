using System.ComponentModel.DataAnnotations;

namespace API.DTOs.contactDTOs
{
    public class ContactRequestDTO
    {
        [Required, MinLength(2)]
        public string Name { get; set; }
        [Required, MinLength(2)]
        public string Lastname { get; set; }
        [Required, MinLength(8), MaxLength(8)]
        public string PhoneNumber { get; set; }
        [Required, MinLength(2)]
        public string Street { get; set; }
        [Required, MinLength(1)]
        public string StreetNumber { get; set; }
        [Required, MinLength(8), MaxLength(8)]
        public string PostalCode { get; set; }
    }
}