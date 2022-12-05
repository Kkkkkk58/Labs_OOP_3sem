using Banks.Commands.Abstractions;
using Banks.Exceptions;
using Banks.Transactions.Abstractions;

namespace Banks.Transactions;

public class CancelledTransactionState : CompletedTransactionState
{
    public CancelledTransactionState(ITransaction transaction)
        : base(transaction)
    {
    }

    public override void Cancel(ICommand command)
    {
        throw TransactionStateException.MultipleCancelling(command.Id);
    }
}