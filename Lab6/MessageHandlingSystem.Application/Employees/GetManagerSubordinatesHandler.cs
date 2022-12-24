using MediatR;
using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Application.Extensions;
using MessageHandlingSystem.Application.Mapping.Employees;
using MessageHandlingSystem.Domain.Employees;
using static MessageHandlingSystem.Application.Contracts.Employees.GetManagerSubordinates;

namespace MessageHandlingSystem.Application.Employees;

public class GetManagerSubordinatesHandler : IRequestHandler<Query, Response>
{
    private readonly IDataBaseContext _dbContext;

    public GetManagerSubordinatesHandler(IDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        Manager manager = await _dbContext.Managers.GetEntityAsync(request.ManagerId, cancellationToken);
        return new Response(manager.Subordinates.Select(x => x.AsDto()));
    }
}