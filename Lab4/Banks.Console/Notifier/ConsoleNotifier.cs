using Banks.Entities.Abstractions;
using Banks.Models;

namespace Banks.Console.Notifier;

public class ConsoleNotifier : ICustomerNotifier
{
    public void Send(Message message)
    {
        System.Console.WriteLine(message);
    }
}