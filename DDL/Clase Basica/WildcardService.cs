using DDL.Interfaces_para_Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DDL.Clase_Basica
{
    public class WildcardService : IWildcardService
    {
        public bool Matches(string input, string pattern)
        {
            var regex = ToRegex(pattern);
            return Regex.IsMatch(input, regex, RegexOptions.IgnoreCase);
        }

        public IEnumerable<string> Filter(IEnumerable<string> items, string pattern)
            => items.Where(item => Matches(item, pattern));

        public string ToRegex(string wildcardPattern)
        {
            var sb = new StringBuilder("^");
            foreach (char c in wildcardPattern)
            {
                switch (c)
                {
                    case '*': sb.Append(".*"); break;
                    case '?': sb.Append("."); break;
                    default: sb.Append(Regex.Escape(c.ToString())); break;
                }
            }
            sb.Append("$");
            return sb.ToString();
        }

        public bool IsGlobPattern(string input)
        {
            return input.Contains('*') || input.Contains('?');
        }
    }

}
