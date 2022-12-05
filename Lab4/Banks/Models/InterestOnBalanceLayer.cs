namespace Banks.Models;

public record InterestOnBalanceLayer
{
    public InterestOnBalanceLayer(MoneyAmount requiredInitialBalance, decimal interestOnBalance)
    {
        if (interestOnBalance < 0)
            throw new ArgumentOutOfRangeException(nameof(interestOnBalance));

        RequiredInitialBalance = requiredInitialBalance;
        InterestOnBalance = interestOnBalance;
    }

    public MoneyAmount RequiredInitialBalance { get; }
    public decimal InterestOnBalance { get; }
}