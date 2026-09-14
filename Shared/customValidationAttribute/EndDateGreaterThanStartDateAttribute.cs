
using System.ComponentModel.DataAnnotations;

namespace Shared.customValidationAttribute;

public class EndDateGreaterThanStartDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(
        object? value,
        ValidationContext validationContext)
    {
        var endDate = value as DateTime?;
        var startDateProperty =
            validationContext.ObjectType.GetProperty("StartDate");

        if (startDateProperty == null)
            return new ValidationResult("StartDate property not found.");

        var startDate = (DateTime?)startDateProperty.GetValue(
            validationContext.ObjectInstance);

        if (endDate.HasValue && startDate.HasValue &&
            endDate.Value <= startDate.Value)
        {
            return new ValidationResult(
                "End date must be greater than start date.");
        }

        return ValidationResult.Success;
    }
}
