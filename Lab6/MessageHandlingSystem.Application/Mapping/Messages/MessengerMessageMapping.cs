using MessageHandlingSystem.Application.Dto.Messages;
using MessageHandlingSystem.Domain.Messages;

namespace MessageHandlingSystem.Application.Mapping.Messages;

public static class MessengerMessageMapping
{
    public static MessengerMessageDto AsDto(this MessengerMessage message)
    {
        return new MessengerMessageDto(
            message.Id,
            message.SendingTime,
            message.HandlingTime,
            message.Content,
            message.State.Kind,
            message.SenderUserName);
    }
}