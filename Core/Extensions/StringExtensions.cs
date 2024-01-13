using System.Text;

namespace Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmptyOrWhiteSpace(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }

        public static string WhereFirstMatchOrDefault(this string value, IEnumerable<string> values, string defaultValue = "") 
        { 
            var match = values.FirstOrDefault(val => value.Contains(val));

            if (!string.IsNullOrEmpty(match))
            {
                return match;
            }

            if (!string.IsNullOrEmpty(defaultValue))
            {
                return defaultValue;
            }
            return string.Empty;
        }
    }
}
