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
        var customerId = args[1].ToGuid();
        var address = new Address(args[2]);

        ICustomer customer = _context.CentralBank.Banks.SelectMany(bank => bank.Customers).Distinct()
            .Single(customer => customer.Id.Equals(customerId));
        customer.SetAddress(address);

        _context.Writer.WriteLine($"Set new address for customer {customerId}");
    }
}