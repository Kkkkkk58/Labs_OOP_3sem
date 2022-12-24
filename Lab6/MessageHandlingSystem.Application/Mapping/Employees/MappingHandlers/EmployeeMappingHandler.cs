using System.Diagnostics.CodeAnalysis;
using MessageHandlingSystem.Application.Dto.Employees;
using MessageHandlingSystem.Application.Mapping.Handlers;
using MessageHandlingSystem.Domain.Employees;

namespace MessageHandlingSystem.Application.Mapping.Employees.MappingHandlers;

public class EmployeeMappingHandler : BaseMappingHandler<Employee, EmployeeDto>
{
    public EmployeeMappingHandler()
    {
        SetNext(new ManagerMappingHandler())
            .SetNext(new SubordinateMappingHandler());
    }

    protected override bool TryHandle(Employee value, [NotNullWhen(true)] out EmployeeDto? result)
    {
        result = null;
        return false;
    }
}