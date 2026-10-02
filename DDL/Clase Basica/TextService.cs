using DDL.Interfaces_para_Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDL.Clase_Basica
{
    public class TextService : ITextService
    {
        private readonly IRegexService _regex;

        public TextService(IRegexService regex)
        {
            _regex = regex;
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            var pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return _regex.IsMatch(email, pattern);
        }

        public bool IsValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return false;
            var pattern = @"^https?://[a-zA-Z0-9\-._~:/?#\[\]@!$&'()*+,;=%]+$";
            return _regex.IsMatch(url, pattern);
        }

        public bool IsValidPhone(string phone, string countryCode = "US")
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            // Simplificado: acepta +1XXXXXXXXXX o (XXX) XXX-XXXX
            var pattern = @"^(\+1[\s.-]?)?(\(\d{3}\)|\d{3})[\s.-]?\d{3}[\s.-]?\d{4}$";
            return _regex.IsMatch(phone, pattern);
        }

        public string Normalize(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            return input.Trim().Replace("  ", " ").ToLowerInvariant();
        }

        public bool ContainsOnlyAlphanumeric(string input)
        {
            if (string.IsNullOrEmpty(input)) return true;
            return input.All(c => char.IsLetterOrDigit(c));
        }

        public bool IsPalindrome(string input)
        {
            if (string.IsNullOrEmpty(input)) return true;
            var cleaned = new string(input.Where(char.IsLetterOrDigit).ToArray()).ToLowerInvariant();
            return cleaned.SequenceEqual(cleaned.Reverse());
        }

        public int CountVowels(string input)
        {
            if (string.IsNullOrEmpty(input)) return 0;
            return input.Count(c => "aeiouAEIOU".Contains(c));
        }

        public string Reverse(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            var chars = input.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
    }

}
