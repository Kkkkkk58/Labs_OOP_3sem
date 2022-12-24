namespace MessageHandlingSystem.Domain.Common.Exceptions;

public class MessageSourceException : MessageHandlingSystemException
{
    private MessageSourceException(string message)
        : base(message)
    {
    }

    public static MessageSourceException InvalidMessageType(Guid messageId, Guid messageSourceId)
    {
        return new MessageSourceException($"Message {messageId} type doesn't fit message source {messageSourceId}");
    }

    public static MessageSourceException MessageAlreadyReceived(Guid messageId, Guid messageSourceId)
    {
        return new MessageSourceException($"Message {messageId} was already received by source {messageSourceId}");
    }
}