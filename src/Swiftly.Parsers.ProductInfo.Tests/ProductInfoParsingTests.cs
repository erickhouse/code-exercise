namespace Swiftly.Parsers.ProductInfo.Tests
{
    using FluentAssertions;
    using Swiftly.Parsers.ProductInfo.Parsers;
    using System.IO;
    using Xunit;

    public class ProductInfoParsingTests
    {
        [Fact]
        public async void ParseProductId_NotNull_Number()
        {
            var data = await File.ReadAllLinesAsync("./Data/input-sample.txt");
            var parser = new ProductInfoParser(new[] { ProductInfoFields.ProductId });
            var product = parser.Parse(data[0]);
            product!.ProductId.Should().Be(80000001);
        }

        [Fact]
        public async void ParseProductDescription_HasExtraPadding_StringNoPadding()
        {
            var data = await File.ReadAllLinesAsync("./Data/input-sample.txt");
            var parser = new ProductInfoParser(new[] { ProductInfoFields.ProductDescription });
            var firstProduct = data[0];
            var product = parser.Parse(firstProduct);
            product!.ProductDescription.Should().Be("Kimchi-flavored white rice");
        }

        [Fact]
        public async void ParseRegularSplitQuantity_NonZeroNumber_NumberIsTwo()
        {
            var data = await File.ReadAllLinesAsync("./Data/input-sample.txt");
            var parser = new ProductInfoParser(new[] { ProductInfoFields.RegularSplitQuantity });
            var secondProduct = data[1];
            var product = parser.Parse(secondProduct);
            product!.RegularSplitQuantity.Should().Be(2);
        }

        [Fact]
        public async void ParseRegularSplitQuantity_AllCharsAreZero_NumberIsZero()
        {
            var data = await File.ReadAllLinesAsync("./Data/input-sample.txt");
            var parser = new ProductInfoParser(new[] { ProductInfoFields.RegularSplitQuantity });
            var thirdProduct = data[2];
            var product = parser.Parse(thirdProduct);
            product!.RegularSplitQuantity.Should().Be(0);
        }

        [Fact]
        public async void ParseRegularEachPrice_NumberIsNonZero_ValueIsFiveSixtySeven()
        {
            var data = await File.ReadAllLinesAsync("./Data/input-sample.txt");
            var parser = new ProductInfoParser(new[] { ProductInfoFields.RegularEachPrice });
            var firstProduct = data[0];
            var product = parser.Parse(firstProduct);
            product!.RegularEachPrice.Should().Be(5.67);
        }

        [Fact]
        public async void ParseSaleEachPrice_NumberIsNonZero_ValueIsFiveFortyNine()
        {
            var data = await File.ReadAllLinesAsync("./Data/input-sample.txt");
            var parser = new ProductInfoParser(new[] { ProductInfoFields.SaleEachPrice });
            var secondProduct = data[1];
            var product = parser.Parse(secondProduct);
            product!.SaleEachPrice.Should().Be(5.49);
        }

        [Fact]
        public async void ParseRegularSplitPrice_NumberIsNonZero_ValueIsThirteen()
        {
            var data = await File.ReadAllLinesAsync("./Data/input-sample.txt");
            var parser = new ProductInfoParser(new[] { ProductInfoFields.RegularSplitPrice });
            var secondProduct = data[1];
            var product = parser.Parse(secondProduct);
            product!.RegularSplitPrice.Should().Be(13);
        }

        [Fact]
        public async void ParseSaleSplitPrice_AllCharsAreZero_NumberIsZero()
        {
            var data = await File.ReadAllLinesAsync("./Data/input-sample.txt");
            var parser = new ProductInfoParser(new[] { ProductInfoFields.SaleSplitPrice });
            var secondProduct = data[1];
            var product = parser.Parse(secondProduct);
            product!.SaleSplitPrice.Should().Be(0);
        }

        [Fact]
        public async void ProductParser_ParseFlags_IsPerWeightItem()
        {
            var data = await File.ReadAllLinesAsync("./Data/input-sample.txt");
            var parser = new ProductInfoParser(new IFieldParser[] { new FlagsParser() });
            var thirdProduct = data[3];
            var product = parser.Parse(thirdProduct);
            product!.Flags!.IsPerWeightItem.Should().BeTrue();
        }

        [Fact]
        public async void ProductParser_ParseFlags_IsTaxable()
        {
            var data = await File.ReadAllLinesAsync("./Data/input-sample.txt");
            var parser = new ProductInfoParser(new IFieldParser[] { new FlagsParser() });
            var secondProduct = data[1];
            var product = parser.Parse(secondProduct);
            product!.Flags!.IsTaxable.Should().BeTrue();
        }
    }
}