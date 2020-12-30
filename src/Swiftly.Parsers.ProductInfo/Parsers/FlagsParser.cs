// unset
namespace Swiftly.Parsers.ProductInfo.Parsers
{
    using System.Collections.Generic;

    public enum ProductFlags
    {
        PerWeightItem = 2,
        Taxable = 4
    }

    public class FlagsParser : IFieldParser
    {
        public int Start { get; } = 124;
        public int End { get; } = 132;

        public void AddData(char[] data, ProductInfo product)
        {
            product.Flags = new ProductInfoFlags
            {
                IsPerWeightItem = ToBoolean(data, ProductFlags.PerWeightItem),
                IsTaxable = ToBoolean(data, ProductFlags.Taxable)
            };
        }

        private static bool ToBoolean(IReadOnlyList<char> data, ProductFlags flag)
        {
            return data[(int)flag] == 'Y';
        }
    }
}