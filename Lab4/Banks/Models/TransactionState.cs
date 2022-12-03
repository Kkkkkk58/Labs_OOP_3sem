using Banks.Commands.Abstractions;
using Banks.Models.Abstractions;

namespace Banks.Models;

public abstract class TransactionState : ITransactionState
{
    protected TransactionState(ITransaction transaction)
    {
        Transaction = transaction;
    }

    protected ITransaction Transaction { get; set; }

    public abstract void Perform(ICommand command);
    public abstract void Cancel(ICommand command);
}