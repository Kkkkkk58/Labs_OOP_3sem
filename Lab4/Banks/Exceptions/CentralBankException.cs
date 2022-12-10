namespace Banks.Exceptions;

public class CentralBankException : BanksException
{
    private CentralBankException(string message)
        : base(message)
    {
    }

    public static CentralBankException BankAlreadyExists(Guid bankId)
    {
        return new CentralBankException($"Bank {bankId} already exists");
    }
}