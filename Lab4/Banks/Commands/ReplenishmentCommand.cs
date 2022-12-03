using Banks.BankAccounts.Abstractions;
using Banks.Commands.Abstractions;
using Banks.Models.Abstractions;

namespace Banks.Commands;

public class ReplenishmentCommand : ICommand
{
    public ReplenishmentCommand()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }

    public void Execute(IBankAccount bankAccount, ITransaction transaction)
    {
        if (!bankAccount.Id.Equals(transaction.Information.AccountId))
            throw new NotImplementedException();

        bankAccount.Replenish(transaction);
    }

    public void Undo(ITransaction transaction)
    {
        transaction.Information.BankAccount.ExecuteCommand(new WithdrawalCommand(), transaction);
    }

    public bool Equals(ICommand? other)
    {
        return other is not null && Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ICommand);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}