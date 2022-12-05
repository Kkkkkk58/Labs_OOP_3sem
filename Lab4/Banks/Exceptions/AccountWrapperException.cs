namespace Banks.Exceptions;

public class AccountWrapperException : BanksException
{
    private AccountWrapperException(string message)
        : base(message)
    {
    }

    public static AccountWrapperException InvalidWrappedType()
    {
        return new AccountWrapperException("Wrapped type is invalid");
    }
}