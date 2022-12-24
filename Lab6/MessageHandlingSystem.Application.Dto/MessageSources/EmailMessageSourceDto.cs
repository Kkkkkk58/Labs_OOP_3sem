using System.Net.Mail;
using MessageHandlingSystem.Application.Dto.Messages;

namespace MessageHandlingSystem.Application.Dto.MessageSources;

public record EmailMessageSourceDto(Guid Id, IReadOnlyCollection<MessageDto> ReceivedMessages, string EmailAddress) : MessageSourceDto(Id, ReceivedMessages);