using Banks.Commands.Abstractions;
using Banks.Models.Abstractions;

namespace Banks.Models;

public class FailedTransactionState : CompletedTransactionState
{
    public FailedTransactionState(ITransaction transaction)
        : base(transaction)
    {
    }

    public override void Cancel(ICommand command)
    {
        throw new NotImplementedException();
    }
}