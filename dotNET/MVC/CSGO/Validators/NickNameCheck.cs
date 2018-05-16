using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CSGO.Models;

namespace CSGO.Validators {
    public class NicknameCheck : ValidationAttribute {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            Player player = (Player)validationContext.ObjectInstance;

            if (player == null)
                throw new ArgumentException("Atribute not applied on Player");

            if (player.Name == player.Nickname || player.Surname == player.Nickname) {
                return new ValidationResult(GetErrorMessage(validationContext));
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage(ValidationContext validationContext) {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return ErrorMessage;

            return "Cannot create player with nickname same as his name or surname";
        }
    }
}
