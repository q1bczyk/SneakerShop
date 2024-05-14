using System.ComponentModel.DataAnnotations;

namespace API.Validators
{
    public class OrderStatusValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string [] status = {"nowe", "zrealizowane"};
            
            if(!status.Contains(value.ToString()))
            return new ValidationResult("Wrong status!");
            
            return ValidationResult.Success;
            
        }
    }
}