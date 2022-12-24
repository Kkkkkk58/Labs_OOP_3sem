using MessageHandlingSystem.Domain.Messages;

namespace MessageHandlingSystem.Domain.MessageSources;

public partial class MessengerMessageSource : MessageSource
{
    public MessengerMessageSource(Guid id, string userName)
    : base(id)
    {
        UserName = userName;
    }

    public string UserName { get; init; }

    protected override void CheckMessageType(Message message)
    {
        if (message is not MessengerMessage)
            throw new NotImplementedException();
    }
}