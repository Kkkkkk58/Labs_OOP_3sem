namespace MessageHandlingSystem.Domain.Common.Exceptions;

public class AccountException : MessageHandlingSystemException
{
    private AccountException(string message)
        : base(message)
    {
    }

    public static AccountException MessageSourceNotFound(Guid messageSourceId, Guid accountId)
    {
        return new AccountException($"Message source {messageSourceId} is not linked to the account {accountId}");
    }

    public static AccountException MessageSourceAlreadyExists(Guid messageSourceId, Guid accountId)
    {
        return new AccountException($"Message source {messageSourceId} is already linked to the account {accountId}");
    }
}