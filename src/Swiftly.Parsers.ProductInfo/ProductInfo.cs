namespace Swiftly.Parsers.ProductInfo
{
    public class ProductInfo
    {
        public int? ProductId { get; set; }

        public string? ProductDescription { get; set; }

        public double? RegularEachPrice { get; set; }

        public double? SaleEachPrice { get; set; }

        public double? RegularSplitPrice { get; set; }

        public double? SaleSplitPrice { get; set; }

        public int? RegularSplitQuantity { get; set; }

        public int? SaleSplitQuantity { get; set; }

        public ProductInfoFlags? Flags { get; set; }

        public string? ProductSize { get; set; }
    }
}