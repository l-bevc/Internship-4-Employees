using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Employess.Infrastructure.Extensions
{
    public static class StringExtension
    {
        public static string FirstCharOfEveryWordToUpper(this string text)
        {
           switch (text)
           {
                case null: throw new ArgumentNullException(nameof(text));
                case "": throw new ArgumentException($"{nameof(text)} cannot be empty", nameof(text));
                default:
                    return Regex.Replace(text, @"\b[a-z]\w+", delegate (Match match)
                    {
                        string v = match.ToString();
                        return char.ToUpper(v[0]) + v.Substring(1);
                    });
           }     
        }

        public static string TrimAndRemoveMultipleWhitespaces(this string text)
        {
            text = text.Trim();
            var multipleWhitespaceRegex = new Regex(@"\s{2,}?");
            while (multipleWhitespaceRegex.IsMatch(text))
                text = multipleWhitespaceRegex.Replace(text, " ");
            return text;
        }
    }
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
