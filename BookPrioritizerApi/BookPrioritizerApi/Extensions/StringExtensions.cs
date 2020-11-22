using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookPrioritizerApi.Extensions
{
    public static class StringExtensions
    {
        public static string LettersOnly(this string str)
        {
            var returnStr = Regex.Replace(str, "[^a-zA-Z]", "");
            return returnStr;
        }
    }
}
