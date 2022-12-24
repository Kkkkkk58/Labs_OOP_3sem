using MediatR;
using MessageHandlingSystem.Application.Dto.Employees;

namespace MessageHandlingSystem.Application.Contracts.Employees;

public static class GetManagerSubordinates
{
    public record struct Query(Guid ManagerId) : IRequest<Response>;

    public record struct Response(IEnumerable<EmployeeDto> Subordinates);
}