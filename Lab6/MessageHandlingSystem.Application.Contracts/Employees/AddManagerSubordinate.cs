using MediatR;

namespace MessageHandlingSystem.Application.Contracts.Employees;

public static class AddManagerSubordinate
{
    public record struct Command(Guid ManagerId, Guid SubordinateId) : IRequest;
}