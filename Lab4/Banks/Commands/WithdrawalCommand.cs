using Banks.BankAccounts.Abstractions;
using Banks.Commands.Abstractions;
using Banks.Models.Abstractions;

namespace Banks.Commands;

public class WithdrawalCommand : ICommand
{
    public WithdrawalCommand()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }

    public void Execute(IBankAccount bankAccount, ITransaction transaction)
    {
        if (!bankAccount.Id.Equals(transaction.Information.AccountId))
            throw new NotImplementedException();

        bankAccount.Withdraw(transaction);
    }

    public void Undo(ITransaction transaction)
    {
        transaction.Information.BankAccount.ExecuteCommand(new ReplenishmentCommand(), transaction);
    }
}