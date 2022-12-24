using System.Net.Mail;
using MediatR;
using MessageHandlingSystem.Application.Dto.Messages;

namespace MessageHandlingSystem.Application.Contracts.Messages;

public static class CreateEmailMessage
{
    public record struct Command
        (DateTime SendingTime, string SenderAddress, string Topic, string Content) : IRequest<Response>;

    public record struct Response(EmailMessageDto Message);
}