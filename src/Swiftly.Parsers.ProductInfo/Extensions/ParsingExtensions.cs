namespace Swiftly.Parsers.ProductInfo.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static  class ParsingExtensions
    {
        public static int ToNumber(this IEnumerable<char> data)
        {
            var noLeadingZero = data.SkipWhile(num => num == '0').ToArray();
            return !noLeadingZero.Any() ? 0 : int.Parse(new string(noLeadingZero));
        }

        public static double ToCurrency(this char[] data)
        {
            var isNegative = data.First() == '-';
            var noLeadingZeroOrDash = data.SkipWhile(num => num == '0' || num == '-').ToArray();
            if (!noLeadingZeroOrDash.Any())
            {
                return 0;
            }

            var number = noLeadingZeroOrDash;
            if (number.Length == 2)
            {
                return flipSign(isNegative, double.Parse("0." + new string(number)));
            }

            var decimalPlace = number.Length - 2;
            var dollars = new string(number[..decimalPlace]);
            var cents = new string(number[decimalPlace..]);
            return flipSign(isNegative, double.Parse($"{dollars}.{cents}"));
        }

        public static string RemovePadding(this char[] data)
        {
            return new string(data).Trim();
        }

        private static double flipSign(bool isNegative, double currency)
        {
            return isNegative ? -currency : currency;
        }
    }
}