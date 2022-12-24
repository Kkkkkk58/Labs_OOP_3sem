using MediatR;
using MessageHandlingSystem.Application.Dto.Messages;

namespace MessageHandlingSystem.Application.Contracts.Accounts;

public static class GetAccountLoadedMessages
{
    public record struct Query(Guid EmployeeId, Guid AccountId) : IRequest<Response>;

    public record struct Response(IEnumerable<MessageDto> Messages);
}