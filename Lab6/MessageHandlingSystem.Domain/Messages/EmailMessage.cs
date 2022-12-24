namespace MessageHandlingSystem.Domain.Messages;

public partial class EmailMessage : Message
{
    public EmailMessage(Guid id, DateTime sendingTime, string senderAddress, string topic, string content)
        : base(id, sendingTime, content)
    {
        SenderAddress = senderAddress;
        Topic = topic;
    }

    public string SenderAddress { get; init; }
    public string Topic { get; init; }
}