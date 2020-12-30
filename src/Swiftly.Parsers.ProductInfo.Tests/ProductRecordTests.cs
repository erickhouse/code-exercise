namespace Swiftly.Parsers.ProductInfo.Tests
{
    using FluentAssertions;
    using Swiftly.Parsers.ProductInfo.Extensions;
    using System.IO;
    using Xunit;

    public class ProductRecordTests
    {
        [Fact]
        public async void CreateRecords_FromInputFile_CreatesFourRecords()
        {
            var data = await File.ReadAllTextAsync("./Data/input-sample.txt");
            var records = ProductRecord.FromFile(data);
            records.Should().HaveCount(4);
        }

        [Fact]
        public void ToRecord_HasTaxableFlag_HasConstantTaxRate()
        {
            var info = new ProductInfo { Flags = new ProductInfoFlags { IsTaxable = true } };
            var actual = info.ToRecord();
            actual!.TaxRate.Should().Be(ProductRecordConstants.TaxRate);
        }

        [Fact]
        public void ToRecord_HasPerWeightFlag_HasPoundUnitOfMeasure()
        {
            var info = new ProductInfo { Flags = new ProductInfoFlags { IsPerWeightItem = true } };
            var actual = info.ToRecord();
            actual!.UnitOfMeasure.Should().Be(ProductRecordUnit.Pound);
        }
    }
}