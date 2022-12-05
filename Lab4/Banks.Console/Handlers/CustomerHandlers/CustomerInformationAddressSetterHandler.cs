using Banks.Console.Extensions;
using Banks.Console.Handlers.Abstractions;
using Banks.Entities.Abstractions;
using Banks.Models;

namespace Banks.Console.Handlers.CustomerHandlers;

public class CustomerInformationAddressSetterHandler : Handler
{
    private readonly AppContext _context;

    public CustomerInformationAddressSetterHandler(AppContext context)
        : base("address")
    {
        _context = context;
    }

    protected override void HandleImpl(string[] args)
    {
        ICustomer customer = GetCustomer();
        Address address = GetAddress();
        customer.SetAddress(address);

        _context.Writer.WriteLine($"Set new address for customer {customer.Id}");
    }

    private ICustomer GetCustomer()
    {
        _context.Writer.Write("Enter customer id: ");
        var customerId = _context.Reader.ReadLine().ToGuid();

        return _context
            .CentralBank
            .Banks
            .SelectMany(bank => bank.Customers)
            .Distinct()
            .Single(customer => customer.Id.Equals(customerId));
    }

    private Address GetAddress()
    {
        _context.Writer.Write("Enter address: ");
        return new Address(_context.Reader.ReadLine());
    }
}