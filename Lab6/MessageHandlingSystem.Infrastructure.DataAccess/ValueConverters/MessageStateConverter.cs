using MessageHandlingSystem.Domain.Messages.MessageStates;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MessageHandlingSystem.Infrastructure.DataAccess.ValueConverters;

public class MessageStateConverter : ValueConverter<IMessageState, MessageStateKind>
{
    public MessageStateConverter()
        : base(x => ConvertTo(x), x => ConvertFrom(x))
    {
    }

    private static MessageStateKind ConvertTo(IMessageState messageState)
    {
        return messageState.Kind;
    }

    private static IMessageState ConvertFrom(MessageStateKind messageStateKind)
    {
        return messageStateKind switch
        {
            MessageStateKind.New => new NewMessageState(),
            MessageStateKind.Loaded => new LoadedMessageState(),
            MessageStateKind.Handled => new HandledMessageState(),
            _ => throw new ArgumentOutOfRangeException(nameof(messageStateKind), messageStateKind, "Cringe"),
        };
    }
}