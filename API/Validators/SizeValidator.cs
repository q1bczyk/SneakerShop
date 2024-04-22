using System.ComponentModel.DataAnnotations;

namespace API.Validators
{
    public class SizeValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var size = (float)value;

            if(size < 30 || size > 50)
                return new ValidationResult("Wrong size range!");

            if(size % 0.5 != 0)
                return new ValidationResult("Size must be divisible by 0.5!");

            return ValidationResult.Success;
        }
    }
}