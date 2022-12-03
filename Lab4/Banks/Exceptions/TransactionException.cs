namespace Banks.Exceptions;

public class TransactionException : BanksException
{
    public TransactionException(string message)
        : base(message)
    {
    }
}