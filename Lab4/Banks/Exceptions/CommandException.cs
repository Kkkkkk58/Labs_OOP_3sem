namespace Banks.Exceptions;

public class CommandException : BanksException
{
    private CommandException(string message)
        : base(message)
    {
    }

    public static CommandException InvalidTransactionData()
    {
        return new CommandException("Transaction data is invalid");
    }
}