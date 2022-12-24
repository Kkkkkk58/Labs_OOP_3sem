using MediatR;
using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Application.Extensions;
using MessageHandlingSystem.Domain.Accounts;
using MessageHandlingSystem.Domain.Employees;
using MessageHandlingSystem.Domain.MessageSources;
using static MessageHandlingSystem.Application.Contracts.Accounts.AddAccountMessageSource;

namespace MessageHandlingSystem.Application.Accounts;

public class AddAccountMessageSourceHandler : IRequestHandler<Command>
{
    private readonly IDataBaseContext _dbContext;

    public AddAccountMessageSourceHandler(IDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
    {
        // TODO single or default check for null
        Employee employee = await _dbContext.Employees.GetEntityAsync(request.EmployeeId, cancellationToken);
        Account account = await _dbContext.Accounts.GetEntityAsync(request.AccountId, cancellationToken);

        if (!employee.Accounts.Contains(account))
            throw new NotImplementedException();
        MessageSource messageSource = await _dbContext.MessageSources.GetEntityAsync(request.MessageSourceId, cancellationToken);
        account.AddMessageSource(messageSource);

        // TODO AddUpdate
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}