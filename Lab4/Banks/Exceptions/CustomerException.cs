namespace Banks.Exceptions;

public class CustomerException : BanksException
{
    private CustomerException(string message)
        : base(message)
    {
    }

    public static CustomerException NotifierIsNotSet(Guid customerId)
    {
        return new CustomerException($"Customer {customerId} has no notifier to add");
    }
}