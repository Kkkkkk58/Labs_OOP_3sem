namespace Banks.Models.AccountTypes.Abstractions;

public interface IFixedInterestOnBalanceSize : IInterestCalculationAccountType
{
    void SetInterestPercent(decimal interestPercent);
}