using System.ComponentModel.DataAnnotations;

namespace MUC.Models.Validations
{
    public class PastDateAttribute : ValidationAttribute
    {
        public string getErrorMessage() => $"Please Pick a date today or later, you can't add a menu in the past";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (DateTime.Now > DateTime.Parse(value.ToString()))
            {
                return new ValidationResult(getErrorMessage());
            }
            return ValidationResult.Success;
        }
    }
}
