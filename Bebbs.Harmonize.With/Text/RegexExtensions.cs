using System;
using System.Text.RegularExpressions;

namespace Bebbs.Harmonize.With.Text
{
    public static class RegexExtensions
    {
        public static string ReadGroupValue(this Match match, string group)
        {
            return ReadGroupAs<string>(match, group, value => value);
        }

        public static T ReadGroupAs<T>(this Match match, string group, Func<string, T> projection)
        {
            if (match.Groups[group].Success)
            {
                try
                {
                    return projection(match.Groups[group].Value);
                }
                catch (Exception)
                {
                    return default(T);
                }
            }
            else
            {
                return default(T);
            }
        }
    }
}
