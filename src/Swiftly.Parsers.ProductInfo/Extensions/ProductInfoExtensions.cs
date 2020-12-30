namespace Swiftly.Parsers.ProductInfo.Extensions
{
    public static class ProductInfoExtensions
    {
        public static ProductRecord ToRecord(this ProductInfo info)
        {
            return new ProductRecord
            {
                ProductId = info.ProductId,
                ProductDescription = info.ProductDescription,
                RegularCalculatorPrice = info.GetRegularPrice(),
                RegularDisplayPrice = info.GetRegularDisplayPrice(),
                SaleCalculatorPrice = info.GetSalePrice(),
                SaleDisplayPrice = info.GetSaleDisplayPrice(),
                TaxRate = info.GetTaxRate(),
                UnitOfMeasure = info.GetUnitOfMeasure(),
                ProductSize = info.ProductSize
            };
        }

        private static double? GetTaxRate(this ProductInfo info)
        {
            var isTaxable = info.Flags?.IsTaxable ?? false;
            return isTaxable ? ProductRecordConstants.TaxRate : null;
        }

        private static ProductRecordUnit GetUnitOfMeasure(this ProductInfo info)
        {
            var byWeight = info.Flags?.IsPerWeightItem ?? false;
            return byWeight ? ProductRecordUnit.Pound : ProductRecordUnit.Each;
        }

        private static string? GetRegularDisplayPrice(this ProductInfo info)
        {
            return FormatDisplayPrice(
                info.RegularEachPrice,
                info.RegularSplitPrice,
                info.RegularSplitQuantity);
        }

        private static string? GetSaleDisplayPrice(this ProductInfo info)
        {
            return FormatDisplayPrice(
                info.SaleEachPrice,
                info.SaleSplitPrice,
                info.SaleSplitQuantity);
        }

        private static double? GetRegularPrice(this ProductInfo info)
        {
            return CalculatePrice(
                info.RegularEachPrice,
                info.RegularSplitPrice,
                info.RegularSplitQuantity);
        }

        private static double? GetSalePrice(this ProductInfo info)
        {
            return CalculatePrice(
                info.SaleEachPrice,
                info.SaleSplitPrice,
                info.SaleSplitQuantity);
        }

        /// <summary>
        /// Each price and split price are mutually exclusive so if the each price
        /// is not null then split must be null.
        /// </summary>
        /// <param name="each">The price per each item.</param>
        /// <param name="split">The price given the quantity.</param>
        /// <param name="quantity">The quantity for the split price.</param>
        /// <returns>The price of one item.</returns>
        private static double? CalculatePrice(double? each, double? split, int? quantity)
        {
            if (each is not null && each != 0)
            {
                return each;
            }

            if (split is not null && quantity is not null && quantity != 0)
            {
                return split / quantity;
            }

            return null;
        }

        /// <summary>
        /// Each price and split price are mutually exclusive so if the each price
        /// is not null then split must be null.
        /// </summary>
        /// <param name="each">The price per each item.</param>
        /// <param name="split">The price given the quantity.</param>
        /// <param name="quantity">The quantity for the split price.</param>
        /// <returns>A formatted display string.</returns>
        private static string? FormatDisplayPrice(double? each, double? split, int? quantity)
        {
            if (each is not null && each != 0)
            {
                return $"${each} each";
            }

            if (split is not null && quantity is not null && each != 0)
            {
                return $"{quantity} for ${split}";
            }

            return null;
        }
    }
}