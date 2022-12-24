using MessageHandlingSystem.Application.Dto.Employees;
using MessageHandlingSystem.Application.Mapping.Employees.MappingHandlers;
using MessageHandlingSystem.Domain.Employees;

namespace MessageHandlingSystem.Application.Mapping.Employees;

public static class EmployeeMapping
{
    public static EmployeeDto AsDto(this Employee employee)
    {
        return new EmployeeMappingHandler().Handle(employee);
    }
}