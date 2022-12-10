namespace Banks.Exceptions;

public class BanksException : ApplicationException
{
    public BanksException(string message)
        : base(message)
    {
    }
}