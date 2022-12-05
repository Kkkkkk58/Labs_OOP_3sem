namespace Banks.Exceptions;

public class BankException : BanksException
{
    private BankException(string message)
        : base(message)
    {
    }

    public static BankException CustomerAlreadyExists(Guid bankId, Guid customerId)
    {
        return new BankException($"Customer {customerId} already exists in bank {bankId}");
    }

    public static BankException CustomerNotFound(Guid customerId)
    {
        return new BankException($"Customer {customerId} not found");
    }

    public static BankException AccountNotFound(Guid accountId)
    {
        return new BankException($"Account {accountId} not found");
    }

    public static BankException InvalidAccountTypeCreation()
    {
        return new BankException("Tried to created an instance of invalid account type");
    }
}