using MediatR;
using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Application.Mapping.Messages;
using MessageHandlingSystem.Domain.Messages;
using static MessageHandlingSystem.Application.Contracts.Messages.CreateEmailMessage;

namespace MessageHandlingSystem.Application.Messages;

public class CreateEmailMessageHandler : IRequestHandler<Command, Response>
{
    private readonly IDataBaseContext _dbContext;

    public CreateEmailMessageHandler(IDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var message = new EmailMessage(
            Guid.NewGuid(),
            request.SendingTime,
            request.SenderAddress,
            request.Topic,
            request.Content);

        await _dbContext.Messages.AddAsync(message, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new Response(message.AsDto());
    }
}