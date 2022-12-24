using MediatR;
using MessageHandlingSystem.Application.Dto.Accounts;

namespace MessageHandlingSystem.Application.Contracts.Employees;

public static class GetEmployeeAccounts
{
    public record struct Query(Guid EmployeeId) : IRequest<Response>;

    public record struct Response(IEnumerable<AccountDto> Accounts);
}