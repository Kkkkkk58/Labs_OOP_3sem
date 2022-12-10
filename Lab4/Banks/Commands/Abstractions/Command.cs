using Banks.BankAccounts.Abstractions;
using Banks.Transactions.Abstractions;

namespace Banks.Commands.Abstractions;

public abstract class Command : ICommand, IEquatable<Command>
{
    protected Command()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }

    public abstract void Execute(IBankAccount bankAccount, ITransaction transaction);
    public abstract void Undo(ITransaction transaction);

    public bool Equals(Command? other)
    {
        return other is not null && Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Command);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}