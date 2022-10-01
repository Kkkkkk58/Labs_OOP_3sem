using Shops.Entities;

namespace Shops.Exceptions;

public class ProductInventoryException : ShopsException
{
    private ProductInventoryException(string message)
        : base(message)
    {
    }

    public static ProductInventoryException ItemNotFoundException(Product product)
    {
        return new ProductInventoryException($"An item with product {product} was not found");
    }

    public static ProductInventoryException MismatchInProductsKvpException()
    {
        return new ProductInventoryException("Some product provided as a key was not part of the inventory item");
    }
}