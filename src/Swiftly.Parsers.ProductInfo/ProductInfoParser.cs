namespace Swiftly.Parsers.ProductInfo
{
    using Swiftly.Parsers.ProductInfo.Parsers;
    using System;
    using System.Text;

    public class ProductInfoParser
    {
        private readonly IFieldParser[] _fields;
        public ProductInfoParser(IFieldParser[]? fields = null)
        {
            _fields = fields ?? ProductInfoFields.All;
        }

        public ProductInfo Parse(string data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var chars = Encoding.ASCII.GetChars(Encoding.ASCII.GetBytes(data));
            var info = new ProductInfo();
            foreach (var parser in _fields)
            {
                (int start, int exclusiveEnd) = GetZeroBasedIndex(parser);
                if (IsOutOfBounds(chars.Length, exclusiveEnd))
                {
                    continue;
                }

                var slice = chars[start..exclusiveEnd];
                parser.AddData(slice, info);
            }

            return info;
        }

        private static (int, int) GetZeroBasedIndex(IFieldParser parser)
        {
            return (parser.Start - 1, parser.End);
        }

        private static bool IsOutOfBounds(int fileLength, int exclusiveEnd)
        {
            return exclusiveEnd >= fileLength;
        }
    }
}