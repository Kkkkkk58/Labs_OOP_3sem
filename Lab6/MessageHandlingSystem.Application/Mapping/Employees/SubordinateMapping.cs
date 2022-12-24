using MessageHandlingSystem.Application.Dto.Employees;
using MessageHandlingSystem.Application.Mapping.Accounts;
using MessageHandlingSystem.Domain.Employees;

namespace MessageHandlingSystem.Application.Mapping.Employees;

public static class SubordinateMapping
{
    public static SubordinateDto AsDto(this Subordinate subordinate)
    {
        return new SubordinateDto(subordinate.Id, subordinate.Name, subordinate.Accounts.Select(x => x.AsDto()).ToArray());
    }
}