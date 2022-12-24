namespace MessageHandlingSystem.Domain.Common.Exceptions;

public class MessageHandlingSystemException : Exception
{
    public MessageHandlingSystemException(string message)
        : base(message)
    {
    }
}