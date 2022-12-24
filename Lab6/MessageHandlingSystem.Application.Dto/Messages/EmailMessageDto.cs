using System.Net.Mail;
using MessageHandlingSystem.Domain.Messages.MessageStates;

namespace MessageHandlingSystem.Application.Dto.Messages;

public record EmailMessageDto(Guid Id, DateTime SendingTime, DateTime? HandlingTime, string Content, MessageStateKind State, string SenderEmailAddress)
    : MessageDto(Id, SendingTime, HandlingTime, Content, State);