using MessageHandlingSystem.Application.Dto.Messages;
using MessageHandlingSystem.Domain.Messages;

namespace MessageHandlingSystem.Application.Mapping.Messages;

public static class PhoneMessageMapping
{
    public static PhoneMessageDto AsDto(this PhoneMessage message)
    {
        return new PhoneMessageDto(
            message.Id,
            message.SendingTime,
            message.HandlingTime,
            message.Content,
            message.State.Kind,
            message.SenderPhoneNumber);
    }
}