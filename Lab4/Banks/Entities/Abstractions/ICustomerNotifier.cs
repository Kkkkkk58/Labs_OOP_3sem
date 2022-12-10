using Banks.Models;

namespace Banks.Entities.Abstractions;

public interface ICustomerNotifier
{
    void Send(Message message);
}