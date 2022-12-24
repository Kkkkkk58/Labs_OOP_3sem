namespace MessageHandlingSystem.Domain.Messages;

public partial class MessengerMessage : Message
{
    public MessengerMessage(Guid id, DateTime sendingTime, string senderUserName, string content)
        : base(id, sendingTime, content)
    {
        SenderUserName = senderUserName;
    }

    public string SenderUserName { get; init; }
}