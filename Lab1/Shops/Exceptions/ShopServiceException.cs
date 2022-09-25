namespace Shops.Exceptions;

public class ShopServiceException : ShopsException
{
    private ShopServiceException(string message)
        : base(message)
    {
    }

    public static ShopServiceException ShopNotFoundException(Guid shopId)
    {
        return new ShopServiceException($"A shop with id={shopId} wasn't found");
    }

    public static ShopServiceException ProductNotFoundException(Guid productId)
    {
        return new ShopServiceException($"A product with id={productId} wasn't found");
    }

    public static ShopServiceException CheapestShopNotFound()
    {
        return new ShopServiceException("A shop with the lowest cost of given shopping list wasn't found");
    }
}