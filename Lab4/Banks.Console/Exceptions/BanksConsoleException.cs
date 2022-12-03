namespace Banks.Console.Exceptions;

public class BanksConsoleException : ApplicationException
{
    public BanksConsoleException(string message)
        : base(message)
    {
    }
}