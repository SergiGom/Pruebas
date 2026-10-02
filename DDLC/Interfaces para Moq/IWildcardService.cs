using System;
using System.Collections.Generic;
using System.Text;

namespace DDL.Interfaces_para_Moq
{
    public interface IWildcardService
    {
        bool Matches(string input, string pattern);
        IEnumerable<string> Filter(IEnumerable<string> items, string pattern);
        string ToRegex(string wildcardPattern);
        bool IsGlobPattern(string input);
    }

}
