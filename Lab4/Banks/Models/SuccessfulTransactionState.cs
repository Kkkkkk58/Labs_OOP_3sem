using Banks.Commands.Abstractions;
using Banks.Models.Abstractions;

namespace Banks.Models;

public class SuccessfulTransactionState : CompletedTransactionState
{
    public SuccessfulTransactionState(ITransaction transaction)
        : base(transaction)
    {
    }

    public override void Cancel(ICommand command)
    {
        command.Undo(Transaction);
        Transaction.ChangeState(new CancelledTransactionState(Transaction));
    }
}