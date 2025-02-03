using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class DebtSplitValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
        {
            return new ValidationResult("Debt split is required.");
        }

        var debtSplit = value.ToString();
        var regex = new Regex(@"^\d+:\d+$");

        if (!regex.IsMatch(debtSplit))
        {
            return new ValidationResult("Debt split must be in the format 'X:Y' where X and Y are integers.");
        }

        return ValidationResult.Success;
    }
}