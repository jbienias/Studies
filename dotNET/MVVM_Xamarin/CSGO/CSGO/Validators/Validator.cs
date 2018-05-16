using CSGO.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace CSGO.Validators
{
    public class Validator
    {
        private static bool ValidateString(string value, string pattern)
        {
            if (value == null || String.IsNullOrEmpty(value))
                return false;
            else if (Regex.IsMatch(value, pattern))
                return true;
            else
                return false;
        }

        private static bool ValidateUrlImage(string url)
        {
            if (url == null || String.IsNullOrEmpty(url))
                return false;
            try
            {
                var req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "HEAD";
                using (var resp = req.GetResponse())
                    return resp.ContentType.ToLower(CultureInfo.InvariantCulture).StartsWith("image/");
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool ValidatePlayer(string name)
        {
            return (ValidateString(name, RegexPatterns.nicknamePattern)
                   || ValidateString(name, RegexPatterns.steamProfilePattern));
        }

        public static bool ValidateTeam(string name)
        {
            return ValidateString(name, RegexPatterns.teamNamePattern);
        }
    }
}
