using System.Net.Mail;
using MediatR;
using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Application.Mapping.MessageSources;
using MessageHandlingSystem.Domain.MessageSources;
using static MessageHandlingSystem.Application.Contracts.MessageSources.CreateEmailMessageSource;

namespace MessageHandlingSystem.Application.MessageSources;

public class CreateEmailMessageSourceHandler : IRequestHandler<Command, Response>
{
    private readonly IDataBaseContext _dbContext;

    public CreateEmailMessageSourceHandler(IDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var source = new EmailMessageSource(Guid.NewGuid(), request.EmailAddress);
        await _dbContext.MessageSources.AddAsync(source, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new Response(source.AsDto());
    }
}