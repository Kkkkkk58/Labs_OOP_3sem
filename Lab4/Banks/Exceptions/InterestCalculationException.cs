namespace Banks.Exceptions;

public class InterestCalculationException : BanksException
{
    private InterestCalculationException(string message)
        : base(message)
    {
    }

    public static InterestCalculationException InvalidUpdateDate()
    {
        return new InterestCalculationException("Update date is invalid");
    }
}