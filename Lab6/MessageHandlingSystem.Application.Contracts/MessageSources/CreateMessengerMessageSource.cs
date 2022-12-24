using MediatR;
using MessageHandlingSystem.Application.Dto.MessageSources;

namespace MessageHandlingSystem.Application.Contracts.MessageSources;

public static class CreateMessengerMessageSource
{
    public record struct Command(string UserName) : IRequest<Response>;

    public record struct Response(MessengerMessageSourceDto Source);
}