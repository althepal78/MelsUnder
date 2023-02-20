using System.ComponentModel.DataAnnotations;

namespace MUC.Models.Validations
{
    public class PastDateAttribute : ValidationAttribute
    {
        public string getErrorMessage() => $"Please pick today's date or a date in the future you can not add a menu in the past";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (DateTime.Now.Date > DateTime.Parse(value.ToString()))
            {
                return new ValidationResult(getErrorMessage());
            }
            return ValidationResult.Success;
        }
    }
}
