using MediatR;
using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Application.Mapping.Employees;
using MessageHandlingSystem.Domain.Employees;
using static MessageHandlingSystem.Application.Contracts.Employees.CreateManager;

namespace MessageHandlingSystem.Application.Employees;

public class CreateManagerHandler : IRequestHandler<Command, Response>
{
    private readonly IDataBaseContext _dbContext;

    public CreateManagerHandler(IDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var manager = new Manager(Guid.NewGuid(), request.Name);
        await _dbContext.Managers.AddAsync(manager, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new Response(manager.AsDto());
    }
}