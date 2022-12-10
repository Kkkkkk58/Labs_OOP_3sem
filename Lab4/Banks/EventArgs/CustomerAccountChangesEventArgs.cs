using Banks.Models;

namespace Banks.EventArgs;

public class CustomerAccountChangesEventArgs : System.EventArgs
{
    public CustomerAccountChangesEventArgs(Message message)
    {
        Message = message;
    }

    public Message Message { get; }
}