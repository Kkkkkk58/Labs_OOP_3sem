using MessageHandlingSystem.Application.Dto.Messages;

namespace MessageHandlingSystem.Application.Dto.MessageSources;

public record PhoneMessageSourceDto(Guid Id, IReadOnlyCollection<MessageDto> ReceivedMessages, string PhoneNumber) : MessageSourceDto(Id, ReceivedMessages);