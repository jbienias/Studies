using CSGO.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace CSGO.Validators.Behaviors
{
    public class TeamNameValidationBehavior : BaseValidationBehavior
    {
        protected override void BindableOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            var name = textChangedEventArgs.NewTextValue;
            var nameEntry = sender as Entry;

            if (Regex.IsMatch(name, RegexPatterns.teamNamePattern))
                nameEntry.TextColor = Color.Black;
            else
                nameEntry.TextColor = Color.Red;
        }
    }
}
