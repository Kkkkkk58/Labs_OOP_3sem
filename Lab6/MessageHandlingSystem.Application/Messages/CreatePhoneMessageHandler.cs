using MediatR;
using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Application.Mapping.Messages;
using MessageHandlingSystem.Domain.Messages;
using static MessageHandlingSystem.Application.Contracts.Messages.CreatePhoneMessage;

namespace MessageHandlingSystem.Application.Messages;

public class CreatePhoneMessageHandler : IRequestHandler<Command, Response>
{
    private readonly IDataBaseContext _dbContext;

    public CreatePhoneMessageHandler(IDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var message =
            new PhoneMessage(Guid.NewGuid(), request.SendingTime, request.SenderPhoneNumber, request.Content);

        await _dbContext.Messages.AddAsync(message, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new Response(message.AsDto());
    }
}