namespace Gradebook.Shared.Attributes;

public class RequiredGuidAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult(ErrorMessage ?? $"The {validationContext.DisplayName} field is required.");
        }

        if (value is Guid guidValue)
        {
            if (guidValue == Guid.Empty)
            {
                return new ValidationResult(ErrorMessage ?? $"The {validationContext.DisplayName} field is required.");
            }
        }
        else
        {
            return new ValidationResult(ErrorMessage ?? $"The {validationContext.DisplayName} field must be a valid GUID.");
        }

        // If validation passes, return Success
        return ValidationResult.Success!;
    }
}