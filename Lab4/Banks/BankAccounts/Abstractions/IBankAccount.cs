using Banks.Models;
using Banks.Models.Abstractions;

namespace Banks.BankAccounts.Abstractions;

public interface IBankAccount : ICommandExecutingBankAccount
{
    MoneyAmount Withdraw(ITransaction transaction);
    void Replenish(ITransaction transaction);
}