namespace Banks.Models;

public class InterestOnBalanceLayer
{
    public InterestOnBalanceLayer(MoneyAmount requiredInitialBalance, decimal interestOnBalance)
    {
        Id = Guid.NewGuid();
        RequiredInitialBalance = requiredInitialBalance;
        InterestOnBalance = interestOnBalance;
    }

    public Guid Id { get; }
    public MoneyAmount RequiredInitialBalance { get; }
    public decimal InterestOnBalance { get; }
}