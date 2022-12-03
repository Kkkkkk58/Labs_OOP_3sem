using Banks.Models;

namespace Banks.Entities.Abstractions;

public interface ICustomer : ISubscriber<CustomerAccountChangesEventArgs>
{
    string FirstName { get; }
    string LastName { get; }
    bool IsVerified { get; }

    Address SetAddress(Address address);
    PassportData SetPassportData(PassportData passportData);
    ICustomerNotifier SetNotifier(ICustomerNotifier notifier);
    ICustomerNotifier AddNotifier(ICustomerNotifierDecoratorBuilder decoratorBuilder);
}