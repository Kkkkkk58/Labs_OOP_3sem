using Banks.Models;

namespace Banks.Entities;

public class CustomerAccountChangesEventArgs : EventArgs
{
    public CustomerAccountChangesEventArgs(Message message)
    {
        Message = message;
    }

    public Message Message { get; }
}