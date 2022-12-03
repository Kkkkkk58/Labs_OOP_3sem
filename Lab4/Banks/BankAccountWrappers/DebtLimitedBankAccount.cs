using Banks.BankAccounts.Abstractions;
using Banks.Models;
using Banks.Models.Abstractions;
using Banks.Models.AccountTypes.Abstractions;

namespace Banks.BankAccountWrappers;

public class DebtLimitedBankAccount : BaseBankAccountWrapper
{
    private readonly IDebtLimitedAccountType _type;

    public DebtLimitedBankAccount(IBankAccount wrapped)
        : base(wrapped)
    {
        if (wrapped.Type is not IDebtLimitedAccountType type)
            throw new NotImplementedException();

        _type = type;
    }

    public override MoneyAmount Withdraw(ITransaction transaction)
    {
        MoneyAmount moneyAmount = transaction.Information.OperatedAmount;

        var debt = new MoneyAmount(0);
        if (Debt.Value != 0)
        {
            debt = Debt + moneyAmount;
        }
        else if (Balance < moneyAmount)
        {
            debt = moneyAmount - Balance;
        }

        if (debt < _type.DebtLimit)
            return base.Withdraw(transaction);

        transaction.Cancel();
        throw new NotImplementedException();
    }
}