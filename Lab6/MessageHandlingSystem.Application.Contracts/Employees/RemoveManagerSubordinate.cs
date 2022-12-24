using MediatR;

namespace MessageHandlingSystem.Application.Contracts.Employees;

public class RemoveManagerSubordinate
{
    public record struct Command(Guid ManagerId, Guid SubordinateId) : IRequest;
}