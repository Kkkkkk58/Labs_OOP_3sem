using Banks.Entities.Abstractions;

namespace Banks.Console.ViewModels;

public class CustomerViewModel
{
    private readonly ICustomer _customer;

    public CustomerViewModel(ICustomer customer)
    {
        _customer = customer;
    }

    public override string ToString()
    {
        return $"Customer {_customer.FirstName}  {_customer.LastName} [{_customer.Id}]\nIs verified: {_customer.IsVerified}";
    }
}