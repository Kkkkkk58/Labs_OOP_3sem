using System.Diagnostics.CodeAnalysis;
using MessageHandlingSystem.Application.Dto.MessageSources;
using MessageHandlingSystem.Application.Mapping.Handlers;
using MessageHandlingSystem.Domain.MessageSources;

namespace MessageHandlingSystem.Application.Mapping.MessageSources.MappingHandlers;

public class MessengerMessageSourceMappingHandler : BaseMappingHandler<MessageSource, MessageSourceDto>
{
    protected override bool TryHandle(MessageSource value, [NotNullWhen(true)] out MessageSourceDto? result)
    {
        result = value is MessengerMessageSource source ? source.AsDto() : null;
        return result is not null;
    }
}