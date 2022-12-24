using MessageHandlingSystem.Application.Dto.MessageSources;
using MessageHandlingSystem.Application.Mapping.MessageSources.MappingHandlers;
using MessageHandlingSystem.Domain.MessageSources;

namespace MessageHandlingSystem.Application.Mapping.MessageSources;

public static class MessageSourceMapping
{
    public static MessageSourceDto AsDto(this MessageSource source)
    {
        return new MessageSourceMappingHandler().Handle(source);
    }
}