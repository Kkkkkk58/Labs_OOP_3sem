using MessageHandlingSystem.Application.Dto.Messages;

namespace MessageHandlingSystem.Application.Dto.MessageSources;

public record MessengerMessageSourceDto(Guid Id, IReadOnlyCollection<MessageDto> ReceivedMessages, string UserName) : MessageSourceDto(Id, ReceivedMessages);