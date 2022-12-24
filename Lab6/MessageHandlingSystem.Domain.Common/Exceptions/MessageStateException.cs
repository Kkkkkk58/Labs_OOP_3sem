namespace MessageHandlingSystem.Domain.Common.Exceptions;

public class MessageStateException : MessageHandlingSystemException
{
    private MessageStateException(string message)
        : base(message)
    {
    }

    public static MessageStateException InvalidMessageState()
    {
        return new MessageStateException("Message state is invalid");
    }
}