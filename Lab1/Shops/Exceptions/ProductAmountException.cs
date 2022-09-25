namespace Shops.Exceptions;

public class ProductAmountException : ShopsException
{
    private ProductAmountException(string message)
        : base(message)
    {
    }

    public static ProductAmountException NegativeAmountException(int amount)
    {
        return new ProductAmountException($"The amount of products must be non-negative but was {amount}");
    }
}