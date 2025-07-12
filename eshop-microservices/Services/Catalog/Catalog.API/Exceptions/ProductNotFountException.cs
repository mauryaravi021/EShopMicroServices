namespace Catalog.API.Exceptions
{
    [Serializable]
    internal class ProductNotFountException : Exception
    {
        public ProductNotFountException() : base("Product Not Found.")
        {
        }
    }
}