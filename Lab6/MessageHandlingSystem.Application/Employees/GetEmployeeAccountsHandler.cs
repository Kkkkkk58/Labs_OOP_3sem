using MediatR;
using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Application.Extensions;
using MessageHandlingSystem.Application.Mapping.Accounts;
using MessageHandlingSystem.Domain.Employees;
using static MessageHandlingSystem.Application.Contracts.Employees.GetEmployeeAccounts;

namespace MessageHandlingSystem.Application.Employees;

public class GetEmployeeAccountsHandler : IRequestHandler<Query, Response>
{
    private readonly IDataBaseContext _dbContext;

    public GetEmployeeAccountsHandler(IDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        Employee employee = await _dbContext.Employees.GetEntityAsync(request.EmployeeId, cancellationToken);
        return new Response(employee.Accounts.Select(x => x.AsDto()));
    }
}