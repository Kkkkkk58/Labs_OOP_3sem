using MediatR;
using MessageHandlingSystem.Application.Dto.Accounts;

namespace MessageHandlingSystem.Application.Contracts.Accounts;

public static class CreateAccount
{
    public record struct Command : IRequest<Response>;

    public record struct Response(AccountDto Account);
}