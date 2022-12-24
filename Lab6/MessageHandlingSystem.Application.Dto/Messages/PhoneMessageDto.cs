using MessageHandlingSystem.Domain.Messages.MessageStates;

namespace MessageHandlingSystem.Application.Dto.Messages;

public record PhoneMessageDto(Guid Id, DateTime SendingTime, DateTime? HandlingTime, string Content, MessageStateKind State, string SenderPhoneNumber)
    : MessageDto(Id, SendingTime, HandlingTime, Content, State);