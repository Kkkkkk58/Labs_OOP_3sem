using Banks.Commands.Abstractions;
using Banks.Exceptions;

namespace Banks.Transactions.Abstractions;

public abstract class CompletedTransactionState : TransactionState
{
    protected CompletedTransactionState(ITransaction transaction)
        : base(transaction)
    {
    }

    public override void Perform(ICommand command)
    {
        throw TransactionStateException.PerformingAfterCompletion(command.Id);
    }
}