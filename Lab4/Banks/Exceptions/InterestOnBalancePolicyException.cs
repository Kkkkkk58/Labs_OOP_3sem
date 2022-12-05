namespace Banks.Exceptions;

public class InterestOnBalancePolicyException : BanksException
{
    private InterestOnBalancePolicyException(string message)
        : base(message)
    {
    }

    public static InterestOnBalancePolicyException LayersWithIntersectionsByInitialBalance()
    {
        return new InterestOnBalancePolicyException("Some layers have the same required balance");
    }
}