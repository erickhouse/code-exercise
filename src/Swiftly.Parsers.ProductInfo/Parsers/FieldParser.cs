// unset
namespace Swiftly.Parsers.ProductInfo.Parsers
{
    using System;

    public class FieldParser : IFieldParser
    {
        private readonly Action<char[], ProductInfo> _setter;

        public FieldParser(int start, int end, Action<char[], ProductInfo> setter)
        {
            Start = start;
            End = end;
            _setter = setter;
        }

        public int Start { get; }
        public int End { get; }

        public void AddData(char[] data, ProductInfo product)
        {
            _setter(data, product);
        }
    }
}