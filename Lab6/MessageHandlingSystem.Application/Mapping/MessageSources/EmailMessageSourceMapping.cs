using MessageHandlingSystem.Application.Dto.MessageSources;
using MessageHandlingSystem.Application.Mapping.Messages;
using MessageHandlingSystem.Domain.MessageSources;

namespace MessageHandlingSystem.Application.Mapping.MessageSources;

public static class EmailMessageSourceMapping
{
    public static EmailMessageSourceDto AsDto(this EmailMessageSource source)
    {
        return new EmailMessageSourceDto(
            source.Id,
            source.ReceivedMessages.Select(x => x.AsDto()).ToArray(),
            source.EmailAddress);
    }
}