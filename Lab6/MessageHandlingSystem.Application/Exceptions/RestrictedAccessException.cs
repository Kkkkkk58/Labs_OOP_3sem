namespace MessageHandlingSystem.Application.Exceptions;

public class RestrictedAccessException : ApplicationException
{
    private RestrictedAccessException(string message)
        : base(message)
    {
    }

    public static RestrictedAccessException NoAccessToAccount(Guid employeeId, Guid accountId)
    {
        return new RestrictedAccessException($"Employee {employeeId} has no access to account {accountId}");
    }
}