namespace Shops.Exceptions;

public class ShopException : ShopsException
{
    private ShopException(string message)
        : base(message)
    {
    }

    public static ShopException EmptyNameException()
    {
        return new ShopException("The name of the shop can't be empty");
    }

    public static ShopException LackOfItemsException(Guid shopId)
    {
        return new ShopException(
            $"The shop with id={shopId} has a lack of resources to buy. Calculation of cost is unavailable");
    }

    public static ShopException MismatchingCurrenciesException(Guid shopId, char shopCurrencySign)
    {
        return new ShopException(
            $"The shop {shopId} expected money amounts with currency {shopCurrencySign} but received other currencies");
    }
}