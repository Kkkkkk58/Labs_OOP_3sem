using MediatR;

namespace MessageHandlingSystem.Application.Contracts.Employees;

public class RemoveEmployeeAccount
{
    public record struct Command(Guid EmployeeId, Guid AccountId) : IRequest;
}