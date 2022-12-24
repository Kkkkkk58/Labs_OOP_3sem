namespace MessageHandlingSystem.Domain.Common.Exceptions;

public class EmployeeException : MessageHandlingSystemException
{
    private EmployeeException(string message)
        : base(message)
    {
    }

    public static EmployeeException AccountAlreadyExists(Guid accountId, Guid employeeId)
    {
        return new EmployeeException($"Account {accountId} is already linked to employee {employeeId}");
    }

    public static EmployeeException AccountNotFound(Guid accountId, Guid employeeId)
    {
        return new EmployeeException($"Account {accountId} was not linked to employee {employeeId}");
    }

    public static EmployeeException SubordinateAlreadyExists(Guid subordinateId, Guid managerId)
    {
        return new EmployeeException($"Subordinate {subordinateId} is already in {managerId} manager's department");
    }

    public static EmployeeException SubordinateNotFound(Guid subordinateId, Guid managerId)
    {
        return new EmployeeException($"Subordinate {subordinateId} was not found in {managerId} manager's department");
    }
}