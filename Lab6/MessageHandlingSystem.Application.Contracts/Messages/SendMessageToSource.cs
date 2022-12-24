using MediatR;

namespace MessageHandlingSystem.Application.Contracts.Messages;

public static class SendMessageToSource
{
    public record struct Command(Guid MessageId, Guid MessageSourceId) : IRequest;
}