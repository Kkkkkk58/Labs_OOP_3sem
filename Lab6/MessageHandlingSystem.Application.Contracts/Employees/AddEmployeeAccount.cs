using MediatR;

namespace MessageHandlingSystem.Application.Contracts.Employees;

public static class AddEmployeeAccount
{
    public record struct Command(Guid EmployeeId, Guid AccountId) : IRequest;
}