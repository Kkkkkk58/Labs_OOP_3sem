using MediatR;
using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Application.Mapping.MessageSources;
using MessageHandlingSystem.Domain.MessageSources;
using static MessageHandlingSystem.Application.Contracts.MessageSources.CreateMessengerMessageSource;

namespace MessageHandlingSystem.Application.MessageSources;

public class CreateMessengerMessageSourceHandler : IRequestHandler<Command, Response>
{
    private readonly IDataBaseContext _dbContext;

    public CreateMessengerMessageSourceHandler(IDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var source = new MessengerMessageSource(Guid.NewGuid(), request.UserName);
        await _dbContext.MessageSources.AddAsync(source, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new Response(source.AsDto());
    }
}