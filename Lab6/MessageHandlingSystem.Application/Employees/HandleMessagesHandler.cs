using MediatR;
using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Application.Commands;
using MessageHandlingSystem.Application.Extensions;
using MessageHandlingSystem.Application.Mapping.Messages;
using MessageHandlingSystem.Domain.Employees;
using MessageHandlingSystem.Domain.Messages;
using static MessageHandlingSystem.Application.Contracts.Employees.HandleMessage;

namespace MessageHandlingSystem.Application.Employees;

public class HandleMessagesHandler : IRequestHandler<Command, Response>
{
    private readonly IDataBaseContext _dbContext;

    public HandleMessagesHandler(IDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        Employee employee = await _dbContext.Employees.GetEntityAsync(request.EmployeeId, cancellationToken);

        employee.HandleMessage(request.AccountId, request.MessageId, new BasicHandlingCommand(), request.Time);
        await _dbContext.SaveChangesAsync(cancellationToken);

        Message message = await _dbContext.Messages.GetEntityAsync(request.MessageId, cancellationToken);
        return new Response(message.AsDto());
    }
}