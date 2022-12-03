using Banks.Console.Chains;
using Banks.Console.Extensions;
using Banks.Entities.Abstractions;
using Banks.Models;

namespace Banks.Console.Controllers.CustomerHandlers;

public class CustomerInformationAddressSetterHandler : Handler
{
    private readonly AppContext _context;

    public CustomerInformationAddressSetterHandler(AppContext context)
        : base("address")
    {
        _context = context;
    }

    public override void Handle(params string[] args)
    {
        var customerId = args[1].ToGuid();
        var address = new Address(args[2]);

        ICustomer customer = _context.CentralBank.Banks.SelectMany(bank => bank.Customers).Distinct()
            .Single(customer => customer.Id.Equals(customerId));
        customer.SetAddress(address);

        base.Handle(args);
    }
}