using CSGO.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace CSGO.Validators
{
    public class BornCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Player player = (Player)validationContext.ObjectInstance;

            if (player == null)
                throw new ArgumentException("Atribute not applied on Player");

            if (player.DateOfBirth > DateTime.Now)
                return new ValidationResult(GetErrorMessage(validationContext));

            return ValidationResult.Success;
        }

        private string GetErrorMessage(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return ErrorMessage;

            return "Cannot create player that has not been born yet or is too old to be alive";
        }
    }
}
