using MediatR;
using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Application.Extensions;
using MessageHandlingSystem.Domain.Accounts;
using MessageHandlingSystem.Domain.Employees;
using static MessageHandlingSystem.Application.Contracts.Accounts.LoadAccountMessages;

namespace MessageHandlingSystem.Application.Accounts;

public class LoadAccountMessagesHandler : IRequestHandler<Command>
{
    private readonly IDataBaseContext _dbContext;

    public LoadAccountMessagesHandler(IDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
    {
        Employee employee = await _dbContext.Employees.GetEntityAsync(request.EmployeeId, cancellationToken);
        Account account = await _dbContext.Accounts.GetEntityAsync(request.AccountId, cancellationToken);

        if (!employee.Accounts.Contains(account))
            throw new NotImplementedException();

        account.LoadMessages(request.MessageSourceId);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}