using Banks.BankAccounts.Abstractions;
using Banks.Commands.Abstractions;
using Banks.Exceptions;
using Banks.Transactions.Abstractions;

namespace Banks.Commands;

public class WithdrawalCommand : Command
{
    public override void Execute(IBankAccount bankAccount, ITransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(bankAccount);
        ArgumentNullException.ThrowIfNull(transaction);

        if (!HaveMatchingAccounts(bankAccount, transaction))
            throw CommandException.InvalidTransactionData();

        bankAccount.Withdraw(transaction);
    }

    public override void Undo(ITransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        transaction.Information.BankAccount.ExecuteCommand(new ReplenishmentCommand(), transaction);
    }

    private static bool HaveMatchingAccounts(IUnchangeableBankAccount bankAccount, ITransaction transaction)
    {
        return bankAccount.Id.Equals(transaction.Information.AccountId);
    }
}