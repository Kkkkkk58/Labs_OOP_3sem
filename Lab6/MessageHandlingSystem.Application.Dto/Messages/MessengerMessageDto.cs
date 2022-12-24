using MessageHandlingSystem.Domain.Messages.MessageStates;

namespace MessageHandlingSystem.Application.Dto.Messages;

public record MessengerMessageDto(Guid Id, DateTime SendingTime, DateTime? HandlingTime, string Content, MessageStateKind State, string SenderUserName)
    : MessageDto(Id, SendingTime, HandlingTime, Content, State);