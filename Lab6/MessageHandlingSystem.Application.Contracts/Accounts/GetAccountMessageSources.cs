using MediatR;
using MessageHandlingSystem.Application.Dto.MessageSources;

namespace MessageHandlingSystem.Application.Contracts.Accounts;

public static class GetAccountMessageSources
{
    public record struct Query(Guid EmployeeId, Guid AccountId) : IRequest<Response>;

    public record struct Response(IEnumerable<MessageSourceDto> Accounts);
}