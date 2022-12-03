using Banks.Commands.Abstractions;
using Banks.Models.Abstractions;

namespace Banks.Models;

public abstract class CompletedTransactionState : TransactionState
{
    protected CompletedTransactionState(ITransaction transaction)
        : base(transaction)
    {
    }

    public override void Perform(ICommand command)
    {
        throw new NotImplementedException();
    }
}