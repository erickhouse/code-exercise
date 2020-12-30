// unset
namespace Swiftly.Parsers.ProductInfo
{
    using Swiftly.Parsers.ProductInfo.Extensions;
    using Swiftly.Parsers.ProductInfo.Parsers;

    public static class ProductInfoFields
    {
        public static readonly IFieldParser[] All =
        {
            ProductId,
            ProductDescription,
            RegularEachPrice,
            SaleEachPrice,
            RegularSplitPrice,
            SaleSplitPrice,
            RegularSplitQuantity,
            SaleSplitQuantity,
            new FlagsParser(),
            ProductSize
        };

        public static IFieldParser ProductId =>
            new FieldParser(1, 8, (chars, data) => data.ProductId = chars.ToNumber());

        public static IFieldParser ProductDescription =>
            new FieldParser(10, 68, (chars, data) => data.ProductDescription = chars.RemovePadding());

        public static IFieldParser RegularEachPrice =>
            new FieldParser(70, 77, (chars, data) => data.RegularEachPrice = chars.ToCurrency());

        public static IFieldParser SaleEachPrice =>
            new FieldParser(79, 86, (chars, data) => data.SaleEachPrice = chars.ToCurrency());

        public static IFieldParser RegularSplitPrice =>
            new FieldParser(88, 95, (chars, data) => data.RegularSplitPrice = chars.ToCurrency());

        public static IFieldParser SaleSplitPrice =>
            new FieldParser(97, 104, (chars, data) => data.SaleSplitPrice = chars.ToCurrency());

        public static IFieldParser RegularSplitQuantity =>
            new FieldParser(106, 113, (chars, data) => data.RegularSplitQuantity = chars.ToNumber());

        public static IFieldParser SaleSplitQuantity =>
            new FieldParser(115, 122, (chars, data) => data.SaleSplitQuantity = chars.ToNumber());

        public static IFieldParser ProductSize =>
            new FieldParser(134, 142, (chars, data) => data.ProductSize = chars.RemovePadding());
    }
}