using MessageHandlingSystem.Application.Dto.Messages;

namespace MessageHandlingSystem.Application.Dto.MessageSources;

public abstract record MessageSourceDto(Guid Id, IReadOnlyCollection<MessageDto> ReceivedMessages);