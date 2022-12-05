using Banks.Models;
using Banks.Transactions.Abstractions;

namespace Banks.BankAccounts.Abstractions;

public interface IBankAccount : ICommandExecutingBankAccount
{
    MoneyAmount Withdraw(ITransaction transaction);
    void Replenish(ITransaction transaction);
}