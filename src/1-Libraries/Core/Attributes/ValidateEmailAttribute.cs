using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Core.Helpers;

namespace CodeBlock.DevKit.Core.Attributes;

public class ValidateEmailAttribute : ValidationAttribute
{
    public ValidateEmailAttribute()
    {
        ErrorMessage = "Invalid email format.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string email && EmailValidator.IsValid(email))
            return ValidationResult.Success;

        // If the validation fails, return the error message from resources or default message.
        return new ValidationResult(ErrorMessageString);
    }
}
