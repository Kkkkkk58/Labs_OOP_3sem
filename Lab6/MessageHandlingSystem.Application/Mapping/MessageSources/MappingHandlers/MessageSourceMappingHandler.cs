using System.Diagnostics.CodeAnalysis;
using MessageHandlingSystem.Application.Dto.MessageSources;
using MessageHandlingSystem.Application.Mapping.Handlers;
using MessageHandlingSystem.Domain.MessageSources;

namespace MessageHandlingSystem.Application.Mapping.MessageSources.MappingHandlers;

public class MessageSourceMappingHandler : BaseMappingHandler<MessageSource, MessageSourceDto>
{
    public MessageSourceMappingHandler()
    {
        SetNext(new EmailMessageSourceMappingHandler())
            .SetNext(new PhoneMessageSourceMappingHandler())
            .SetNext(new MessengerMessageSourceMappingHandler());
    }

    protected override bool TryHandle(MessageSource value, [NotNullWhen(true)] out MessageSourceDto? result)
    {
        result = null;
        return false;
    }
}