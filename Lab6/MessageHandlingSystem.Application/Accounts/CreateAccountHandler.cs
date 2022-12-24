using MediatR;
using MessageHandlingSystem.Application.Abstractions.DataAccess;
using MessageHandlingSystem.Application.Mapping.Accounts;
using MessageHandlingSystem.Domain.Accounts;
using static MessageHandlingSystem.Application.Contracts.Accounts.CreateAccount;

namespace MessageHandlingSystem.Application.Accounts;

public class CreateAccountHandler : IRequestHandler<Command, Response>
{
    private readonly IDataBaseContext _dbContext;

    public CreateAccountHandler(IDataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        var account = new Account(Guid.NewGuid());
        await _dbContext.Accounts.AddAsync(account, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new Response(account.AsDto());
    }
}