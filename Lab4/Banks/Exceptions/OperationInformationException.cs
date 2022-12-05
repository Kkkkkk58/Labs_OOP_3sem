namespace Banks.Exceptions;

public class OperationInformationException : BanksException
{
    private OperationInformationException(string message)
        : base(message)
    {
    }

    public static OperationInformationException IsAlreadyCompleted()
    {
        return new OperationInformationException($"Operation is already completed");
    }
}