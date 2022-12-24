using MessageHandlingSystem.Application.Dto.MessageSources;
using MessageHandlingSystem.Application.Mapping.Messages;
using MessageHandlingSystem.Domain.MessageSources;

namespace MessageHandlingSystem.Application.Mapping.MessageSources;

public static class MessengerMessageSourceMapping
{
    public static MessengerMessageSourceDto AsDto(this MessengerMessageSource source)
    {
        return new MessengerMessageSourceDto(
            source.Id,
            source.ReceivedMessages.Select(x => x.AsDto()).ToArray(),
            source.UserName);
    }
}