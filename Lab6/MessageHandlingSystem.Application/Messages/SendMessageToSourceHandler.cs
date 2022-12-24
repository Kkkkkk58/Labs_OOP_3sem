using MediatR;
using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Application.Extensions;
using MessageHandlingSystem.Domain.Messages;
using MessageHandlingSystem.Domain.MessageSources;
using static MessageHandlingSystem.Application.Contracts.Messages.SendMessageToSource;
namespace MessageHandlingSystem.Application.Messages;

public class SendMessageToSourceHandler : IRequestHandler<Command>
{
    private readonly IDataBaseContext _dbContext;

    public SendMessageToSourceHandler(IDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
    {
        Message message = await _dbContext.Messages.GetEntityAsync(request.MessageId, cancellationToken);
        MessageSource source = await _dbContext.MessageSources.GetEntityAsync(request.MessageSourceId, cancellationToken);

        source.ReceiveMessage(message);
        _dbContext.Messages.Update(message);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}