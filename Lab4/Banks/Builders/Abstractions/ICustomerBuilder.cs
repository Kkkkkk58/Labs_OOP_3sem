using Banks.Entities.Abstractions;
using Banks.Models;

namespace Banks.Builders.Abstractions;

public interface ICustomerBuilder
{
    ICustomerLastNameBuilder SetFirstName(string firstName);
}

public interface ICustomerLastNameBuilder
{
    IOptionalCustomerInformationBuilder SetLastName(string lastName);
}

public interface IOptionalCustomerInformationBuilder
{
    IOptionalCustomerInformationBuilder SetAddress(Address address);
    IOptionalCustomerInformationBuilder SetPassportData(PassportData passportData);
    IOptionalCustomerInformationBuilder SetNotifier(ICustomerNotifier notifier);
    ICustomer Build();
}