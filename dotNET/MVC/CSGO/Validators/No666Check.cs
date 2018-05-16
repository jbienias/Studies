using CSGO.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace CSGO.Validators
{
    public class No666Check : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Player player = (Player)validationContext.ObjectInstance;

            if (player == null)
                throw new ArgumentException("Atribute not applied on Player");

            if (player.Nickname.Contains("666"))
                return new ValidationResult(GetErrorMessage(validationContext));

            return ValidationResult.Success;
        }

        private string GetErrorMessage(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return ErrorMessage;

            return "Our site creator is hexakosioihexekontahexaphobic!";
        }
    }
}
