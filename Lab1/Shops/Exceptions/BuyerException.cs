namespace Shops.Exceptions;

public class BuyerException : ShopsException
{
    private BuyerException(string message)
        : base(message)
    {
    }

    public static BuyerException EmptyNameException()
    {
        return new BuyerException("Buyer's name can't be empty");
    }
}