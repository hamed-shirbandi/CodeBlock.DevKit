// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using System.ComponentModel.DataAnnotations;
using CodeBlock.DevKit.Core.Helpers;

namespace CodeBlock.DevKit.Core.Attributes;

public class ValidateEmailAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is string email && EmailValidator.IsValid(email))
        {
            return ValidationResult.Success;
        }

        // Retrieve the display name from the context, if available
        var displayName = validationContext.DisplayName;

        // Format the error message using the display name, replacing {0} with it
        var errorMessage = string.Format(ErrorMessageString ?? "{0} is not valid", displayName);

        return new ValidationResult(errorMessage);
    }
}

