using MediatR;
using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Application.Exceptions;
using MessageHandlingSystem.Application.Extensions;
using MessageHandlingSystem.Application.Mapping.Messages;
using MessageHandlingSystem.Domain.Accounts;
using MessageHandlingSystem.Domain.Employees;
using static MessageHandlingSystem.Application.Contracts.Accounts.GetAccountLoadedMessages;

namespace MessageHandlingSystem.Application.Accounts;

public class GetAccountLoadedMessagesHandler : IRequestHandler<Query, Response>
{
    private readonly IDataBaseContext _dbContext;

    public GetAccountLoadedMessagesHandler(IDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        Employee employee = await _dbContext.Employees.GetEntityAsync(request.EmployeeId, cancellationToken);
        Account account = await _dbContext.Accounts.GetEntityAsync(request.AccountId, cancellationToken);
        if (!employee.Accounts.Contains(account))
            throw RestrictedAccessException.NoAccessToAccount(employee.Id, account.Id);

        return await new Task<Response>(() => new Response(account.LoadedMessages.Select(x => x.AsDto()).ToArray()));
    }
}