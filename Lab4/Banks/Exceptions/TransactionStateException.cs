namespace Banks.Exceptions;

public class TransactionStateException : BanksException
{
    private TransactionStateException(string message)
        : base(message)
    {
    }

    public static TransactionStateException MultipleCancelling(Guid commandId)
    {
        return new TransactionStateException($"Cannot cancel operation {commandId} twice");
    }

    public static TransactionStateException PerformingAfterCompletion(Guid commandId)
    {
        return new TransactionStateException($"Cannot perform completed operation {commandId}");
    }

    public static TransactionStateException CancellingFailedOperation(Guid commandId)
    {
        return new TransactionStateException($"Cannot cancel failed operation {commandId}");
    }

    public static TransactionStateException CancellingNewOperation(Guid commandId)
    {
        return new TransactionStateException($"Cannot cancel an operation {commandId} that wasn't performed");
    }

    public static TransactionStateException AlreadyRunningOperation(Guid commandId)
    {
        return new TransactionStateException($"Operation {commandId} is already running");
    }
}