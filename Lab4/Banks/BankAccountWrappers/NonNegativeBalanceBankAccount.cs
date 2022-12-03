using Banks.BankAccounts.Abstractions;
using Banks.Models;
using Banks.Models.Abstractions;

namespace Banks.BankAccountWrappers;

public class NonNegativeBalanceBankAccount : BaseBankAccountWrapper
{
    public NonNegativeBalanceBankAccount(IBankAccount wrapped)
        : base(wrapped)
    {
    }

    public override MoneyAmount Withdraw(ITransaction transaction)
    {
        if (Balance < transaction.Information.OperatedAmount)
        {
            transaction.Cancel();
        }

        return base.Withdraw(transaction);
    }
}