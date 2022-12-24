using MediatR;
using MessageHandlingSystem.Application.Dto.Employees;

namespace MessageHandlingSystem.Application.Contracts.Employees;

public class GetEmployeeInformation
{
    public record struct Query(Guid EmployeeId) : IRequest<Response>;

    public record struct Response(EmployeeDto Employee);
}