using MediatR;
using MessageHandlingSystem.Application.Dto.Messages;

namespace MessageHandlingSystem.Application.Contracts.Employees;

public static class HandleMessage
{
    public record struct Command(Guid EmployeeId, Guid AccountId,Guid MessageId, DateTime Time) : IRequest<Response>;

    public record struct Response(MessageDto HandledMessage);
}