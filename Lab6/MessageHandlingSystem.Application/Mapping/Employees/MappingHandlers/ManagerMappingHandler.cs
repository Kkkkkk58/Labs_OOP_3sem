using System.Diagnostics.CodeAnalysis;
using MessageHandlingSystem.Application.Dto.Employees;
using MessageHandlingSystem.Application.Mapping.Handlers;
using MessageHandlingSystem.Domain.Employees;

namespace MessageHandlingSystem.Application.Mapping.Employees.MappingHandlers;

public class ManagerMappingHandler : BaseMappingHandler<Employee, EmployeeDto>
{
    protected override bool TryHandle(Employee value, [NotNullWhen(true)] out EmployeeDto? result)
    {
        result = value is Manager manager ? manager.AsDto() : null;

        return result is not null;
    }
}