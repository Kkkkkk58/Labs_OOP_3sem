using Banks.Commands.Abstractions;
using Banks.Exceptions;
using Banks.Transactions.Abstractions;

namespace Banks.Transactions;

public class FailedTransactionState : CompletedTransactionState
{
    public FailedTransactionState(ITransaction transaction)
        : base(transaction)
    {
    }

    public override void Cancel(ICommand command)
    {
        throw TransactionStateException.CancellingFailedOperation(command.Id);
    }
}