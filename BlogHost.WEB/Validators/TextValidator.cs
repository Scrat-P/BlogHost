using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BlogHost.WEB.Validators
{
    public static class TextValidator
    {
        public static bool ValidateTitle(string text)
        {
            string pattern = @"^[-\w\s.,!?\n]+$";
            return Regex.IsMatch(text, pattern);
        }
    }
}
