namespace Swiftly.Parsers.ProductInfo.Parsers
{
    public interface IFieldParser
    {
        /// <summary>
        /// Gets the field data start.
        /// </summary>
        public int Start { get; }

        /// <summary>
        /// Gets the field data end [inclusive].
        /// </summary>
        public int End { get; }

        /// <summary>
        /// Parses the string data and adds the result
        /// to the product data object.
        /// </summary>
        /// <param name="data">The data to parse.</param>
        /// <param name="product">The mutated product data.</param>
        public void AddData(char[] data, ProductInfo product);
    }
}