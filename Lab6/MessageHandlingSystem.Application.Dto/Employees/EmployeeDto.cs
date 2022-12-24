using MessageHandlingSystem.Application.Dto.Accounts;

namespace MessageHandlingSystem.Application.Dto.Employees;

public abstract record EmployeeDto(Guid Id, string Name, IReadOnlyCollection<AccountDto> Accounts);