using MediatR;
using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Application.Exceptions;
using MessageHandlingSystem.Application.Extensions;
using MessageHandlingSystem.Application.Mapping.MessageSources;
using MessageHandlingSystem.Domain.Accounts;
using MessageHandlingSystem.Domain.Employees;
using static MessageHandlingSystem.Application.Contracts.Accounts.GetAccountMessageSources;

namespace MessageHandlingSystem.Application.Accounts;

public class GetAccountMessageSourcesHandler : IRequestHandler<Query, Response>
{
    private readonly IDataBaseContext _dbContext;

    public GetAccountMessageSourcesHandler(IDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        Employee employee = await _dbContext.Employees.GetEntityAsync(request.EmployeeId, cancellationToken);
        Account account = await _dbContext.Accounts.GetEntityAsync(request.AccountId, cancellationToken);
        if (!employee.Accounts.Contains(account))
            throw RestrictedAccessException.NoAccessToAccount(employee.Id, account.Id);

        return await new Task<Response>(() => new Response(account.MessageSources.Select(x => x.AsDto()).ToArray()));
    }
}