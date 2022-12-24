using MediatR;
using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Application.Mapping.Messages;
using MessageHandlingSystem.Domain.Messages;
using static MessageHandlingSystem.Application.Contracts.Messages.CreateMessengerMessage;

namespace MessageHandlingSystem.Application.Messages;

public class CreateMessengerMessageHandler : IRequestHandler<Command, Response>
{
    private readonly IDataBaseContext _dbContext;

    public CreateMessengerMessageHandler(IDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var message = new MessengerMessage(Guid.NewGuid(), request.SendingTime, request.SenderUserName, request.Content);
        await _dbContext.Messages.AddAsync(message, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new Response(message.AsDto());
    }
}