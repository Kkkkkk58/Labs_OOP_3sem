namespace Shops.Exceptions;

public class ShopsException : ApplicationException
{
    public ShopsException(string message)
        : base(message)
    {
    }
}