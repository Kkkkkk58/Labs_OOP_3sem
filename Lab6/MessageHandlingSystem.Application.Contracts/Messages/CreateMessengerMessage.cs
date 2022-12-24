using MediatR;
using MessageHandlingSystem.Application.Dto.Messages;

namespace MessageHandlingSystem.Application.Contracts.Messages;

public static class CreateMessengerMessage
{
    public record struct Command
        (DateTime SendingTime, string SenderUserName, string Content) : IRequest<Response>;

    public record struct Response(MessengerMessageDto Message);
}