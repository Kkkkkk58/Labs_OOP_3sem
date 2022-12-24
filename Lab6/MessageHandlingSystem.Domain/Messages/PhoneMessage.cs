namespace MessageHandlingSystem.Domain.Messages;

public partial class PhoneMessage : Message
{
    public PhoneMessage(Guid id, DateTime sendingTime, string senderPhoneNumber, string content)
        : base(id, sendingTime, content)
    {
        SenderPhoneNumber = senderPhoneNumber;
    }

    public string SenderPhoneNumber { get; init; }
}