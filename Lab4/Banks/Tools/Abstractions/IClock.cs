using Banks.EventArgs;

namespace Banks.Tools.Abstractions;

public interface IClock
{
    DateTime Now { get; }
    void Subscribe(Action<object?, DateChangedEventArgs> update);
}