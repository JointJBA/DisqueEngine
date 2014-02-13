using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace DisqueRenderToy
{
    public static class StringExtensions
    {
        private static Regex oneOrMoreWhitespaces = new Regex(@"\s+", RegexOptions.Compiled);
        public static string[] SplitOnWhitespace(this string input)
        {
            return oneOrMoreWhitespaces.Split(input.Trim());
        }
        public static string EnumerateToString(this IEnumerable items, string prefix = null, string separator = " ")
        {
            var builder = new StringBuilder();
            foreach (var item in items)
            {
                if (builder.Length > 0)
                {
                    builder.Append(separator);
                }

                if (prefix != null)
                {
                    builder.Append(prefix);
                }

                builder.Append(item);
            }

            return builder.ToString();
        }
    }
}
