namespace Banks.Exceptions;

public class AccountTypeManagerException : BanksException
{
    private AccountTypeManagerException(string message)
        : base(message)
    {
    }

    public static AccountTypeManagerException InvalidAccountType()
    {
        return new AccountTypeManagerException("Provided invalid account type");
    }
}