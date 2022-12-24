using MessageHandlingSystem.Application.Dto.Employees;
using MessageHandlingSystem.Application.Mapping.Accounts;
using MessageHandlingSystem.Domain.Employees;

namespace MessageHandlingSystem.Application.Mapping.Employees;

public static class ManagerMapping
{
    public static ManagerDto AsDto(this Manager manager)
    {
        return new ManagerDto(manager.Id, manager.Name, manager.Accounts.Select(x => x.AsDto()).ToArray(), manager.Subordinates.Select(x => x.AsDto()).ToArray());
    }
}