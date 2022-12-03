using Banks.BankAccounts.Abstractions;
using Banks.Models;
using Banks.Models.Abstractions;
using Banks.Models.AccountTypes.Abstractions;

namespace Banks.BankAccountWrappers;

public class ChargeableAfterDebtBankAccount : BaseBankAccountWrapper
{
    private readonly IChargeableAccountType _type;

    public ChargeableAfterDebtBankAccount(IBankAccount wrapped)
        : base(wrapped)
    {
        if (wrapped.Type is not IChargeableAccountType type)
            throw new NotImplementedException();

        _type = type;
    }

    public override MoneyAmount Withdraw(ITransaction transaction)
    {
        ApplyCharge(transaction);

        return base.Withdraw(transaction);
    }

    public override void Replenish(ITransaction transaction)
    {
        ApplyCharge(transaction);

        base.Replenish(transaction);
    }

    private void ApplyCharge(ITransaction transaction)
    {
        if (Debt.Value == 0)
        {
            return;
        }

        MoneyAmount moneyAmount = transaction.Information.OperatedAmount;
        if (moneyAmount < _type.Charge)
        {
            // TODO CHANGE STATES FROM REF/GURU, throw ex
            transaction.Cancel();
            throw new NotImplementedException();
        }

        transaction.Information.SetOperatedAmount(moneyAmount - _type.Charge);
    }
}