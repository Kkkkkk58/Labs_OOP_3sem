using MessageHandlingSystem.Domain.Common.Exceptions;
using MessageHandlingSystem.Domain.Messages;
using RichEntity.Annotations;

namespace MessageHandlingSystem.Domain.MessageSources;

public abstract partial class MessageSource : IEntity<Guid>
{
    private readonly List<Message> _receivedMessages;

    protected MessageSource(Guid id)
    {
        Id = id;
        _receivedMessages = new List<Message>();
    }

    public virtual IReadOnlyCollection<Message> ReceivedMessages => _receivedMessages.AsReadOnly();

    public Message ReceiveMessage(Message message)
    {
        CheckMessageType(message);

        if (_receivedMessages.Contains(message))
            throw MessageSourceException.MessageAlreadyReceived(message.Id, Id);

        _receivedMessages.Add(message);
        return message;
    }

    protected abstract void CheckMessageType(Message message);
}