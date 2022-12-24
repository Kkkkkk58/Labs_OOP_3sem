using MediatR;
using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Application.Extensions;
using MessageHandlingSystem.Domain.Employees;
using static MessageHandlingSystem.Application.Contracts.Employees.RemoveManagerSubordinate;

namespace MessageHandlingSystem.Application.Employees;

public class RemoveManagerSubordinateHandler : IRequestHandler<Command>
{
    private readonly IDataBaseContext _dbContext;

    public RemoveManagerSubordinateHandler(IDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
    {
        Manager manager = await _dbContext.Managers.GetEntityAsync(request.ManagerId, cancellationToken);
        Employee employee = await _dbContext.Employees.GetEntityAsync(request.SubordinateId, cancellationToken);

        manager.RemoveSubordinate(employee);
        _dbContext.Managers.Update(manager);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}