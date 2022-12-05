using Banks.BankAccounts.Abstractions;
using Banks.Commands.Abstractions;
using Banks.Transactions.Abstractions;

namespace Banks.Commands;

public class WithdrawalCommand : Command
{
    public override void Execute(IBankAccount bankAccount, ITransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(bankAccount);
        ArgumentNullException.ThrowIfNull(transaction);

        bankAccount.Withdraw(transaction);
    }

    public override void Undo(ITransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        transaction.Information.BankAccount.ExecuteCommand(new ReplenishmentCommand(), transaction);
    }
}