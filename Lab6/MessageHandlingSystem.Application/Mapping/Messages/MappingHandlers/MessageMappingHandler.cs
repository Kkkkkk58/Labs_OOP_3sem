using System.Diagnostics.CodeAnalysis;
using MessageHandlingSystem.Application.Dto.Messages;
using MessageHandlingSystem.Application.Mapping.Handlers;
using MessageHandlingSystem.Domain.Messages;

namespace MessageHandlingSystem.Application.Mapping.Messages.MappingHandlers;

public class MessageMappingHandler : BaseMappingHandler<Message, MessageDto>
{
    public MessageMappingHandler()
    {
        SetNext(new EmailMessageMappingHandler())
            .SetNext(new PhoneMessageMappingHandler())
            .SetNext(new MessengerMessageMappingHandler());
    }

    protected override bool TryHandle(Message value, [NotNullWhen(true)] out MessageDto? result)
    {
        result = null;
        return false;
    }
}