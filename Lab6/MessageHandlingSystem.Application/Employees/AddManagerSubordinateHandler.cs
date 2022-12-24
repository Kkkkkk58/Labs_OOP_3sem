using MediatR;
using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Application.Extensions;
using MessageHandlingSystem.Domain.Employees;
using static MessageHandlingSystem.Application.Contracts.Employees.AddManagerSubordinate;
namespace MessageHandlingSystem.Application.Employees;

public class AddManagerSubordinateHandler : IRequestHandler<Command>
{
    private readonly IDataBaseContext _dbContext;

    public AddManagerSubordinateHandler(IDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
    {
        Manager manager = await _dbContext.Managers.GetEntityAsync(request.ManagerId, cancellationToken);
        Employee subordinate = await _dbContext.Employees.GetEntityAsync(request.SubordinateId, cancellationToken);

        manager.AddSubordinate(subordinate);
        _dbContext.Managers.Update(manager);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}