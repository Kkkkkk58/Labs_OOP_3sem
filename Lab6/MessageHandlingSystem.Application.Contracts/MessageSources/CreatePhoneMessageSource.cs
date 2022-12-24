using MediatR;
using MessageHandlingSystem.Application.Dto.MessageSources;

namespace MessageHandlingSystem.Application.Contracts.MessageSources;

public static class CreatePhoneMessageSource
{
    public record struct Command(string PhoneNumber) : IRequest<Response>;

    public record struct Response(PhoneMessageSourceDto Source);
}