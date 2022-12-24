using MessageHandlingSystem.Application.Dto.Accounts;

namespace MessageHandlingSystem.Application.Dto.Employees;

public record ManagerDto(Guid Id, string Name, IReadOnlyCollection<AccountDto> Accounts, IReadOnlyCollection<EmployeeDto> Subordinates)
    : EmployeeDto(Id, Name, Accounts);