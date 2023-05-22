using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SGTest.Services
{
    internal static class StringCleaner
    {
        public static string Clean(string value)
        {
            if (value == String.Empty) return value;
            var removedWhiteSpaces = Regex.Replace(value.Trim(), "[ ]+", " ");
            var fixedRegistr = removedWhiteSpaces.ToLower().ToCharArray();
            fixedRegistr[0] = char.ToUpper(fixedRegistr[0]);
            return new string(fixedRegistr);
        }

        public static string Clean(string[] values)
        {
            for(var i = 0; i < values.Length; i++)
            {
                values[i] = Clean(values[i]);
            }

            return String.Join(" ", values);
        }
    }
}
