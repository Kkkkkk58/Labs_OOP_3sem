using MediatR;
using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Application.Extensions;
using MessageHandlingSystem.Domain.Accounts;
using MessageHandlingSystem.Domain.Employees;
using static MessageHandlingSystem.Application.Contracts.Employees.RemoveEmployeeAccount;
namespace MessageHandlingSystem.Application.Employees;

public class RemoveEmployeeAccountHandler : IRequestHandler<Command>
{
    private readonly IDataBaseContext _dbContext;

    public RemoveEmployeeAccountHandler(IDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
    {
        Employee employee = await _dbContext.Employees.GetEntityAsync(request.EmployeeId, cancellationToken);
        Account account = await _dbContext.Accounts.GetEntityAsync(request.AccountId, cancellationToken);

        employee.RemoveAccount(account);
        _dbContext.Employees.Update(employee);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}