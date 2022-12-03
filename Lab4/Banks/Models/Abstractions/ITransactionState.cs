using Banks.Commands.Abstractions;

namespace Banks.Models.Abstractions;

public interface ITransactionState
{
    void Perform(ICommand command);
    void Cancel(ICommand command);
}