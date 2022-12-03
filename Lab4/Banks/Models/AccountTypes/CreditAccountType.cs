using Banks.Models.AccountTypes.Abstractions;

namespace Banks.Models.AccountTypes;

public class CreditAccountType : SuspiciousLimitingAccountType, ICreditAccountType
{
    public CreditAccountType(MoneyAmount debtLimit, MoneyAmount charge, MoneyAmount suspiciousAccountsOperationsLimit)
        : base(suspiciousAccountsOperationsLimit)
    {
        DebtLimit = debtLimit;
        Charge = charge;
    }

    public MoneyAmount DebtLimit { get; private set; }
    public MoneyAmount Charge { get; private set; }

    public void SetDebtLimit(MoneyAmount debtLimit)
    {
        DebtLimit = debtLimit;
    }

    public void SetCharge(MoneyAmount charge)
    {
        Charge = charge;
    }
}