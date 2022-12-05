using Banks.Commands.Abstractions;

namespace Banks.Transactions.Abstractions;

public interface ITransactionState
{
    void Perform(ICommand command);
    void Cancel(ICommand command);
}