namespace Shops.Exceptions;

public class ProductException : ShopsException
{
    private ProductException(string message)
        : base(message)
    {
    }

    public static ProductException EmptyNameException()
    {
        return new ProductException("The name of the product can't be empty");
    }
}