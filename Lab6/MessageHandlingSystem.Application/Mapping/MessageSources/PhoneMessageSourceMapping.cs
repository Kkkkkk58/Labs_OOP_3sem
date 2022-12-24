using MessageHandlingSystem.Application.Dto.MessageSources;
using MessageHandlingSystem.Application.Mapping.Messages;
using MessageHandlingSystem.Domain.MessageSources;

namespace MessageHandlingSystem.Application.Mapping.MessageSources;

public static class PhoneMessageSourceMapping
{
    public static PhoneMessageSourceDto AsDto(this PhoneMessageSource source)
    {
        return new PhoneMessageSourceDto(
            source.Id,
            source.ReceivedMessages.Select(x => x.AsDto()).ToArray(),
            source.PhoneNumber);
    }
}