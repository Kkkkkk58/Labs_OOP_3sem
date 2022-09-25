namespace Shops.Exceptions;

public class AddressException : ShopsException
{
    private AddressException(string message)
        : base(message)
    {
    }

    public static AddressException EmptyAddressException()
    {
        throw new AddressException("Address can't be empty");
    }
}