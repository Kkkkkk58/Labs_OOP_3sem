using MessageHandlingSystem.Application.Dto.Messages;
using MessageHandlingSystem.Application.Dto.MessageSources;

namespace MessageHandlingSystem.Application.Dto.Accounts;

public record AccountDto(Guid Id, IReadOnlyCollection<MessageSourceDto> MessageSources, IReadOnlyCollection<MessageDto> LoadedMessages);