namespace Swiftly.Parsers.ProductInfo
{
    using Swiftly.Parsers.ProductInfo.Extensions;
    using System.Collections.Generic;

    public enum ProductRecordUnit
    {
        Each,
        Pound
    }

    public class ProductRecord
    {
        public static List<ProductRecord> FromFile(string fileContents)
        {
            var parser = new ProductInfoParser();
            var records = new List<ProductRecord>();
            foreach (var line in fileContents.Split("\n"))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    records.Add(parser.Parse(line).ToRecord());
                }
            }

            return records;
        }

        public int? ProductId { get; init; }

        public string? ProductDescription { get; init; }

        public string? RegularDisplayPrice { get; init; }

        public double? RegularCalculatorPrice { get; init; }

        public double? SaleCalculatorPrice { get; init; }

        public string? SaleDisplayPrice { get; init; }

        public string? ProductSize { get; init; }

        public ProductRecordUnit UnitOfMeasure { get; init; }

        public double? TaxRate { get; init; }
    }
}