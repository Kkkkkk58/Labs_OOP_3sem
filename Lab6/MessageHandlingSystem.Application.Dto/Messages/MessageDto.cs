using MessageHandlingSystem.Domain.Messages.MessageStates;

namespace MessageHandlingSystem.Application.Dto.Messages;

public abstract record MessageDto(Guid Id, DateTime SendingTime, DateTime? HandlingTime, string Content, MessageStateKind State);