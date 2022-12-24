using MediatR;
using MessageHandlingSystem.Application.Dto.MessageSources;

namespace MessageHandlingSystem.Application.Contracts.MessageSources;

public static class CreateEmailMessageSource
{
    public record struct Command(string EmailAddress) : IRequest<Response>;

    public record struct Response(EmailMessageSourceDto Source);
}