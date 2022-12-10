using Banks.AccountTypes.Abstractions;
using Banks.Models;

namespace Banks.AccountTypes;

public class DepositAccountType : SuspiciousLimitingAccountType, IDepositAccountType
{
    public DepositAccountType(
        InterestOnBalancePolicy interestOnBalancePolicy,
        TimeSpan depositTerm,
        TimeSpan interestCalculationPeriod,
        MoneyAmount suspiciousAccountsOperationsLimit)
        : base(suspiciousAccountsOperationsLimit)
    {
        InterestOnBalancePolicy = interestOnBalancePolicy;
        DepositTerm = depositTerm;
        InterestCalculationPeriod = interestCalculationPeriod;
    }

    public InterestOnBalancePolicy InterestOnBalancePolicy { get; private set; }
    public TimeSpan DepositTerm { get; private set; }
    public TimeSpan InterestCalculationPeriod { get; private set; }

    public void SetInterestOnBalancePolicy(InterestOnBalancePolicy interestOnBalancePolicy)
    {
        ArgumentNullException.ThrowIfNull(interestOnBalancePolicy);
        InterestOnBalancePolicy = interestOnBalancePolicy;
    }

    public void SetDepositTerm(TimeSpan depositTerm)
    {
        DepositTerm = depositTerm;
    }

    public void SetInterestCalculationPeriod(TimeSpan interestCalculationPeriod)
    {
        InterestCalculationPeriod = interestCalculationPeriod;
    }

    public decimal GetInterestPercent(MoneyAmount initialBalance)
    {
        return InterestOnBalancePolicy
            .Layers
            .LastOrDefault(layer => layer.RequiredInitialBalance <= initialBalance)?
            .InterestOnBalance
                ?? 0;
    }
}