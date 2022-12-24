using MediatR;
using MessageHandlingSystem.Application.Dto.Messages;

namespace MessageHandlingSystem.Application.Contracts.Messages;

public static class CreatePhoneMessage
{
    public record struct Command
        (DateTime SendingTime, string SenderPhoneNumber, string Content) : IRequest<Response>;

    public record struct Response(PhoneMessageDto Message);
}