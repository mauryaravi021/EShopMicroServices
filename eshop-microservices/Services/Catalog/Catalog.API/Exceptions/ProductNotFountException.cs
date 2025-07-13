namespace Catalog.API.Exceptions
{
    [Serializable]
    internal class ProductNotFountException : NotFoundException
    {
        public ProductNotFountException(Guid Id) : base("Product", Id)
        {
        }
    }
}