using MessageHandlingSystem.Application.Dto.Accounts;

namespace MessageHandlingSystem.Application.Dto.Employees;

public record SubordinateDto(Guid Id, string Name, IReadOnlyCollection<AccountDto> Accounts)
    : EmployeeDto(Id, Name, Accounts);