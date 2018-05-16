using CSGO.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace CSGO.Validators
{
    public class DateCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Team team = (Team)validationContext.ObjectInstance;

            if (team == null)
                throw new ArgumentException("Atribute not applied on Team");

            if (team.DateOfFounding > DateTime.Now)
                return new ValidationResult(GetErrorMessage(validationContext));

            return ValidationResult.Success;
        }

        private string GetErrorMessage(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return ErrorMessage;

            return "Cannot create team that has not been founded yet";
        }
    }
}
