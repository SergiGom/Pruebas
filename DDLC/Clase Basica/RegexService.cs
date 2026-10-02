using DDL.Interfaces_para_Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DDL.Clase_Basica
{
    public class RegexService : IRegexService
    {
        public bool IsMatch(string input, string pattern, RegexOptions options = RegexOptions.None)
        {
            try
            {
                return Regex.IsMatch(input, pattern, options);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public Match? TryMatch(string input, string pattern)
        {
            try
            {
                return Regex.Match(input, pattern).Success ? Regex.Match(input, pattern) : null;
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        public IEnumerable<string> ExtractAll(string input, string pattern)
        {
            try
            {
                return Regex.Matches(input, pattern)
                           .Cast<Match>()
                           .Select(m => m.Value);
            }
            catch (ArgumentException)
            {
                return Enumerable.Empty<string>();
            }
        }

        public string Replace(string input, string pattern, string replacement)
        {
            try
            {
                return Regex.Replace(input, pattern, replacement);
            }
            catch (ArgumentException)
            {
                return input;
            }
        }

        public bool IsValidPattern(string pattern)
        {
            try
            {
                _ = new Regex(pattern);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public IEnumerable<string> Split(string input, string pattern)
        {
            try
            {
                return Regex.Split(input, pattern);
            }
            catch (ArgumentException)
            {
                return new[] { input };
            }
        }
    }

}
