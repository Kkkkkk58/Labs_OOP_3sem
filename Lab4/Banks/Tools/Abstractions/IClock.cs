using Banks.BankAccountWrappers;

namespace Banks.Tools.Abstractions;

// TODO Use IObservable
public interface IClock
{
    DateTime Now { get; }
    void Subscribe(Action<object?, DateChangedEventArgs> update);
}