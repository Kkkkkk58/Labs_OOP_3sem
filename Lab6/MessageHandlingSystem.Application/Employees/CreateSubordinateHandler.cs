using MediatR;
using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Application.Mapping.Employees;
using MessageHandlingSystem.Domain.Employees;
using static MessageHandlingSystem.Application.Contracts.Employees.CreateSubordinate;

namespace MessageHandlingSystem.Application.Employees;

public class CreateSubordinateHandler : IRequestHandler<Command, Response>
{
    private readonly IDataBaseContext _dbContext;

    public CreateSubordinateHandler(IDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var subordinate = new Subordinate(Guid.NewGuid(), request.Name);
        await _dbContext.Subordinates.AddAsync(subordinate, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new Response(subordinate.AsDto());
    }
}