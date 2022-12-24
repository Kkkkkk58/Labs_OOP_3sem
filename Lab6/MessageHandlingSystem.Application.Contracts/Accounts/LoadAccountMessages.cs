using MediatR;

namespace MessageHandlingSystem.Application.Contracts.Accounts;

public static class LoadAccountMessages
{
    public record struct Command(Guid EmployeeId, Guid AccountId, Guid MessageSourceId) : IRequest;
}