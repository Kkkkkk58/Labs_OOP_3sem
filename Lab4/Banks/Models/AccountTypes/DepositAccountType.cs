using Banks.Entities;
using Banks.Models.AccountTypes.Abstractions;

namespace Banks.Models.AccountTypes;

public class DepositAccountType : SuspiciousLimitingAccountType, IDepositAccountType
{
    public DepositAccountType(InterestOnBalancePolicy interestOnBalancePolicy, TimeSpan depositTerm, TimeSpan interestCalculationPeriod, MoneyAmount suspiciousAccountsOperationsLimit)
        : base(suspiciousAccountsOperationsLimit)
    {
        InterestOnBalancePolicy = interestOnBalancePolicy;
        DepositTerm = depositTerm;
        InterestCalculationPeriod = interestCalculationPeriod;
    }

    public InterestOnBalancePolicy InterestOnBalancePolicy { get; }
    public TimeSpan DepositTerm { get; private set; }
    public TimeSpan InterestCalculationPeriod { get; private set; }

    public InterestOnBalanceLayer AddLayer(InterestOnBalanceLayer layer)
    {
        throw new NotImplementedException();
    }

    public void RemoveLayer(InterestOnBalanceLayer layer)
    {
        throw new NotImplementedException();
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
            .First(layer => layer.RequiredInitialBalance <= initialBalance)
            .InterestOnBalance;
    }
}