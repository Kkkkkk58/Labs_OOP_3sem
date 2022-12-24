using MediatR;
using MessageHandlingSystem.Application.Dto.Employees;

namespace MessageHandlingSystem.Application.Contracts.Employees;

public static class CreateManager
{
    public record struct Command(string Name) : IRequest<Response>;

    public record struct Response(ManagerDto Manager);
}