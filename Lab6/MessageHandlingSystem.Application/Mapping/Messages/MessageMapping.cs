using MessageHandlingSystem.Application.Dto.Messages;
using MessageHandlingSystem.Application.Mapping.Messages.MappingHandlers;
using MessageHandlingSystem.Domain.Messages;

namespace MessageHandlingSystem.Application.Mapping.Messages;

public static class MessageMapping
{
    public static MessageDto AsDto(this Message message)
    {
        return new MessageMappingHandler().Handle(message);
    }
}