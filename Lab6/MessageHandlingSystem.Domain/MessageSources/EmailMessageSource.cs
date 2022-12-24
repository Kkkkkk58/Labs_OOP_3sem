using MessageHandlingSystem.Domain.Common.Exceptions;
using MessageHandlingSystem.Domain.Messages;

namespace MessageHandlingSystem.Domain.MessageSources;

public partial class EmailMessageSource : MessageSource
{
    public EmailMessageSource(Guid id, string emailAddress)
        : base(id)
    {
        EmailAddress = emailAddress;
    }

    public string EmailAddress { get; init; }

    protected override void CheckMessageType(Message message)
    {
        if (message is not EmailMessage)
            throw MessageSourceException.InvalidMessageType(message.Id, Id);
    }
}