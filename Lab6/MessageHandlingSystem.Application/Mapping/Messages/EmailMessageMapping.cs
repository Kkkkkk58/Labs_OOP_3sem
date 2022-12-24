using MessageHandlingSystem.Application.Dto.Messages;
using MessageHandlingSystem.Domain.Messages;

namespace MessageHandlingSystem.Application.Mapping.Messages;

public static class EmailMessageMapping
{
    public static EmailMessageDto AsDto(this EmailMessage message)
    {
        return new EmailMessageDto(
            message.Id,
            message.SendingTime,
            message.HandlingTime,
            message.Content,
            message.State.Kind,
            message.SenderAddress);
    }
}