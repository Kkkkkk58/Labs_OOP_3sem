using Banks.AccountTypes.Abstractions;
using Banks.Models;

namespace Banks.AccountTypes;

public class DebitAccountType : SuspiciousLimitingAccountType, IDebitAccountType
{
    private decimal _interestPercent;

    public DebitAccountType(
        decimal interestOnBalance,
        TimeSpan interestCalculationPeriod,
        MoneyAmount suspiciousAccountsOperationsLimit)
        : base(suspiciousAccountsOperationsLimit)
    {
        if (interestOnBalance < 0)
            throw new ArgumentOutOfRangeException(nameof(interestOnBalance));

        InterestCalculationPeriod = interestCalculationPeriod;
        _interestPercent = interestOnBalance;
    }

    public TimeSpan InterestCalculationPeriod { get; private set; }

    public void SetInterestCalculationPeriod(TimeSpan interestCalculationPeriod)
    {
        InterestCalculationPeriod = interestCalculationPeriod;
    }

    public decimal GetInterestPercent(MoneyAmount initialBalance)
    {
        return _interestPercent;
    }

    public void SetInterestPercent(decimal interestPercent)
    {
        if (interestPercent < 0)
            throw new ArgumentOutOfRangeException(nameof(interestPercent));

        _interestPercent = interestPercent;
    }
}