using System.Diagnostics.CodeAnalysis;
using MessageHandlingSystem.Application.Dto.Messages;
using MessageHandlingSystem.Application.Mapping.Handlers;
using MessageHandlingSystem.Domain.Messages;

namespace MessageHandlingSystem.Application.Mapping.Messages.MappingHandlers;

public class MessengerMessageMappingHandler : BaseMappingHandler<Message, MessageDto>
{
    protected override bool TryHandle(Message value, [NotNullWhen(true)] out MessageDto? result)
    {
        result = value is MessengerMessage message ? message.AsDto() : null;
        return result is not null;
    }
}