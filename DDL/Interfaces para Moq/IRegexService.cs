using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DDL.Interfaces_para_Moq
{
    public interface IRegexService
    {
        bool IsMatch(string input, string pattern, RegexOptions options = RegexOptions.None);
        Match? TryMatch(string input, string pattern);
        IEnumerable<string> ExtractAll(string input, string pattern);
        string Replace(string input, string pattern, string replacement);
        bool IsValidPattern(string pattern);
        IEnumerable<string> Split(string input, string pattern);
    }

}
