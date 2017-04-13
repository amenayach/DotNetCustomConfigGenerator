using System;

namespace ConfigGenerator.ObjectModel
{
    /// <summary>
    /// Manage string functionalities
    /// </summary>
    public static class StringManager
    {

        /// <summary>
        /// Retruns true if string is not empty
        /// </summary>
        public static bool NotEmpty(this string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }

        /// <summary>
        /// Retruns true if string is empty
        /// </summary>
        public static bool IsEmpty(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }
        
        /// <summary>
        /// Capitalize first letter
        /// </summary>
        public static string CapitalizeFirstLetter(this string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                return text[0].ToString().ToUpper() + (text.Length > 1 ? text.Substring(1) : string.Empty);
            }

            return text;
        }
        
        /// <summary>
        /// Retruns splitted string by the upper case letters
        /// </summary>
        public static string SplitCamelCase(this string text)
        {
            var r = new System.Text.RegularExpressions.Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace);

            return r.Replace(text, " ");
        }

        /// <summary>
        /// Retruns splitted string by string instead of char
        /// </summary>
        public static string[] SplitString(this string text, string splitter)
        {
            return string.IsNullOrEmpty(text) ? new string[0] : text.Split(new string[] { splitter }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Retruns trimmed string or empty string when IsNullOrWhiteSpace
        /// </summary>
        public static string NullTrimer(this string text)
        {
            return string.IsNullOrWhiteSpace(text) ? string.Empty : text.ToString().Trim();
        }

    }
}
