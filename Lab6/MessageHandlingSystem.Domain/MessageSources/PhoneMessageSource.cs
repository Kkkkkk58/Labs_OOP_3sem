using MessageHandlingSystem.Domain.Common.Exceptions;
using MessageHandlingSystem.Domain.Messages;

namespace MessageHandlingSystem.Domain.MessageSources;

public partial class PhoneMessageSource : MessageSource
{
    public PhoneMessageSource(Guid id, string phoneNumber)
        : base(id)
    {
        PhoneNumber = phoneNumber;
    }

    public string PhoneNumber { get; init; }

    protected override void CheckMessageType(Message message)
    {
        if (message is not PhoneMessage)
            throw MessageSourceException.InvalidMessageType(message.Id, Id);
    }
}