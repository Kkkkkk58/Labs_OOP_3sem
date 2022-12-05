using Banks.BankAccounts.Abstractions;
using Banks.Exceptions;
using Banks.Models;
using Banks.Transactions.Abstractions;

namespace Banks.BankAccounts.Wrappers;

public class NonNegativeBalanceBankAccount : BaseBankAccountWrapper
{
    public NonNegativeBalanceBankAccount(IBankAccount wrapped)
        : base(wrapped)
    {
    }

    public override MoneyAmount Withdraw(ITransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);
        if (Balance < transaction.Information.OperatedAmount)
            throw TransactionException.NegativeBalance();

        return base.Withdraw(transaction);
    }
}